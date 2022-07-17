using System.Collections;
using Slothsoft.UnityExtensions;
using UnityEditor;
using UnityEngine;

namespace MizuKiri {
    public class StoneSpawner : MonoBehaviour {
        [SerializeField, Expandable]
        Transform container = default;
        [SerializeField, Expandable]
        StoneFactory factory = default;

        [SerializeField]
        float spawnInterval = 1;
        [SerializeField]
        float spawnRadius = 1;

        protected IEnumerator Start() {
            while (true) {
                SpawnStone();
                yield return Wait.forSeconds[spawnInterval];
            }
        }

        void SpawnStone() {
            var stone = factory.InstantiateStone(transform.position + (spawnRadius * Random.insideUnitSphere));
            stone.transform.SetParent(container, true);
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }

#if UNITY_EDITOR
        [SerializeField]
        int stoneCount = 100;
        [SerializeField]
        PhysicsStepper physics = default;
        [SerializeField]
        DiveWater diveWater = default;

        IEnumerator SpawnStones() {
            for (int i = 0; i < stoneCount; i++) {
                SpawnStone();
                if (physics) {
                    yield return physics.Simulate();
                } else {
                    yield return null;
                }
            }
        }

        void CullInvalid() {
            foreach (var stone in diveWater.containingStones) {
                Debug.Log($"Killing stone {stone.position}");
                DestroyImmediate(stone.gameObject);
            }
        }

        [CustomEditor(typeof(StoneSpawner))]
        class StoneSpawnerEditor : RuntimeEditorTools<StoneSpawner> {
            protected override void DrawEditorTools() {
                DrawButton("Spawn Stone", target.SpawnStone);
                DrawButton("Spawn Stones", target.SpawnStones);
                DrawButton("Cull invalid!", target.CullInvalid);
            }
        }
#endif
    }
}
