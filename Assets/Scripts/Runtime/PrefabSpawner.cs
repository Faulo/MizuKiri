using UnityEngine;

namespace MizuKiri {
    public class PrefabSpawner : MonoBehaviour {
        public void Instantiate(Vector3 position) {
            Instantiate(this, position, Quaternion.identity);
        }
    }
}