using System.Collections;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri {
    public class StoneSpawner : MonoBehaviour {
        [SerializeField, Expandable]
        StoneFactory factory = default;

        [SerializeField, Expandable]
        Stone prefab = default;

        [SerializeField]
        float spawnInterval = 1;

        protected IEnumerator Start() {
            while (true) {
                SpawnStone();
                yield return Wait.forSeconds[spawnInterval];
            }
        }

        public Stone SpawnStone() {
            var stone = Instantiate(prefab, transform.position, Random.rotation);

            stone.mesh = factory.randomMesh;
            stone.materials = factory.randomMaterials;

            return stone;
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
    }
}
