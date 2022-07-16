using UnityEngine;

namespace MizuKiri {
    public class ParticleController : MonoBehaviour {
        public void Instantiate(Vector3 position) {
            Instantiate(this, position, Quaternion.identity);
        }
    }
}