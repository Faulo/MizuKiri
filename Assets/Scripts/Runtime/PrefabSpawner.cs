using UnityEngine;

namespace MizuKiri {
    public class PrefabSpawner : MonoBehaviour {
        public void Instantiate(Vector3 position) {
            Instantiate(this, position, Quaternion.identity);
        }
        public void Instantiate(Stone stone) {
            Instantiate(this, stone.position, Quaternion.identity);
        }
    }
}