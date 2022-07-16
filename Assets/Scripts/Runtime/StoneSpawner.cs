using System.Collections;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri {
    public class StoneSpawner : MonoBehaviour {
        [SerializeField, Expandable]
        StoneFactory factory = default;

        [SerializeField]
        float spawnInterval = 1;

        protected IEnumerator Start() {
            while (true) {
                SpawnStone();
                yield return Wait.forSeconds[spawnInterval];
            }
        }

        void SpawnStone() {
            factory.InstantiateStone(transform.position);
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
    }
}
