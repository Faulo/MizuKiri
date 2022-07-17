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
        int spawnCount = 100;
        [SerializeField]
        float spawnInterval = 1;
        [SerializeField]
        float spawnRadius = 1;

        public void SpawnStones() {
            StartCoroutine(SpawnStones_Co());
        }

        IEnumerator SpawnStones_Co() {
            for (int i = 0; i < spawnCount; i++) {
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
        PhysicsStepper physics = default;
        [SerializeField]
        DiveWater diveWater = default;

        IEnumerator SpawnStoneWithPhysicss_Co() {
            for (int i = 0; i < spawnCount; i++) {
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
                DrawButton("Spawn Stones", target.SpawnStoneWithPhysicss_Co);
                DrawButton("Cull invalid!", target.CullInvalid);
            }
        }
#endif
    }
}
