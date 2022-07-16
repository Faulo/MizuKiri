using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GK;
using MyBox;
using Slothsoft.UnityExtensions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace MizuKiri {
    [CreateAssetMenu]
    public class StoneFactory : ScriptableObject {
        [Serializable]
        class StoneSetting {
            [SerializeField]
            Vector3 scale = Vector3.one;

            public Vector3 randomVertex => Vector3.Scale(UnityEngine.Random.insideUnitSphere, scale);
        }
        [Serializable]
        class MaterialSetting {
            [SerializeField, Expandable]
            Material rendering = default;
            [SerializeField, Expandable]
            PhysicMaterial physics = default;

            public (Material rendering, PhysicMaterial physics) tuple => (rendering, physics);
        }

        [SerializeField, Expandable]
        Stone prefab = default;

        Quaternion randomRotation => Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

        [SerializeField, ReadOnly]
        Mesh[] meshes = Array.Empty<Mesh>();

        [SerializeField]
        MaterialSetting[] materials = Array.Empty<MaterialSetting>();

        Mesh randomMesh => meshes.RandomElement();

        (Material rendering, PhysicMaterial physics) randomMaterials => materials.RandomElement().tuple;

        public Stone InstantiateStone(Vector3 position) {
            var stone = Instantiate(prefab, position, randomRotation);

            stone.mesh = randomMesh;
            stone.materials = randomMaterials;

            return stone;
        }

#if UNITY_EDITOR
        [Header("Factory settings")]
        [SerializeField, Range(1, 10000)]
        int stonesToPrepare = 1000;

        [Header("Stone settings")]
        [SerializeField, Range(3, 100)]
        int triangleCount = 10;
        [SerializeField]
        bool bakePhysicsMesh = false;
        [SerializeField]
        StoneSetting[] settings = Array.Empty<StoneSetting>();

        Vector3 randomUV => new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);


        string assetPath => AssetDatabase.GetAssetPath(this);

        static readonly ConvexHullCalculator calculator = new();

        Mesh CreateRandomMesh() {
            var points = new Vector3[3 * triangleCount];
            var setting = settings.RandomElement();
            for (int i = 0; i < 3 * triangleCount; i++) {
                points[i] = setting.randomVertex;
            }

            var verts = new List<Vector3>();
            var tris = new List<int>();
            var normals = new List<Vector3>();

            calculator.GenerateHull(points, true, ref verts, ref tris, ref normals);

            var mesh = new Mesh();

            mesh.SetVertices(verts);
            mesh.SetTriangles(tris, 0);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, Unwrapping.GeneratePerTriangleUV(mesh));
            mesh.SetUVs(1, Enumerable.Repeat(randomUV, verts.Count).ToList());

            if (bakePhysicsMesh) {
                Physics.BakeMesh(mesh.GetInstanceID(), true);
            }

            return mesh;
        }

        IEnumerator ClearStones() {
            meshes = Array.Empty<Mesh>();

            foreach (var mesh in AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Mesh>()) {
                AssetDatabase.RemoveObjectFromAsset(mesh);
                DestroyImmediate(mesh);
                yield return null;
            }

            AssetDatabase.SaveAssets();
        }

        IEnumerator PrepareStones() {
            yield return ClearStones();

            meshes = new Mesh[stonesToPrepare];

            for (int i = 0; i < stonesToPrepare; i++) {
                meshes[i] = CreateRandomMesh();
                meshes[i].name = $"Stone #{i + 1}";

                AssetDatabase.AddObjectToAsset(meshes[i], this);

                yield return null;
            }

            AssetDatabase.SaveAssets();
        }

        [CustomEditor(typeof(StoneFactory))]
        class StoneFactoryEditor : RuntimeEditorTools<StoneFactory> {
            protected override void DrawEditorTools() {
                DrawButton("Clear Stones", target.ClearStones);
                DrawButton("Prepare Stones", target.PrepareStones);
            }
        }
#endif
    }
}