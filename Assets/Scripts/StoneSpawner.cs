using System.Collections;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri {
    public class StoneSpawner : MonoBehaviour {
        [SerializeField, Expandable]
        StoneFactory factory = default;

        [SerializeField, Expandable]
        GameObject prefab = default;

        [SerializeField]
        float spawnInterval = 1;

        protected IEnumerator Start() {
            while (true) {
                SpawnStone();
                yield return Wait.forSeconds[spawnInterval];
            }
        }

        void SpawnStone() {
            var obj = Instantiate(prefab, transform.position, Random.rotation);

            var filter = obj.GetComponent<MeshFilter>();
            var collider = obj.GetComponent<MeshCollider>();
            var renderer = obj.GetComponent<MeshRenderer>();
            var rigidbody = obj.GetComponent<Rigidbody>();


            var mesh = factory.CreateStoneMesh();

            filter.sharedMesh = mesh;
            collider.sharedMesh = mesh;
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
    }
}
