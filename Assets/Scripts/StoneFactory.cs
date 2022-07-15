using System.Collections.Generic;
using GK;
using UnityEngine;

namespace MizuKiri {
    [CreateAssetMenu]
    public class StoneFactory : ScriptableObject {
        [SerializeField]
        int triangleCount = 10;
        [SerializeField]
        bool bakeMesh = false;

        [SerializeField]
        Vector3 scale = Vector3.one;

        Vector3 randomVertex => Vector3.Scale(Random.insideUnitSphere, scale);

        static readonly ConvexHullCalculator calculator = new();

        public Mesh CreateStoneMesh() {

            var points = new Vector3[3 * triangleCount];
            for (int i = 0; i < 3 * triangleCount; i++) {
                points[i] = randomVertex;
            }

            var verts = new List<Vector3>();
            var tris = new List<int>();
            var normals = new List<Vector3>();

            calculator.GenerateHull(points, true, ref verts, ref tris, ref normals);

            var mesh = new Mesh {
                name = "Stone"
            };

            mesh.SetVertices(verts);
            mesh.SetTriangles(tris, 0);
            mesh.SetNormals(normals);

            if (bakeMesh) {
                Physics.BakeMesh(mesh.GetInstanceID(), true);
            }

            return mesh;
        }
    }
}