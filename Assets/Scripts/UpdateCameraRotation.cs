using UnityEngine;

namespace MizuKiri {
    public class UpdateCameraRotation : MonoBehaviour {
        [SerializeField]
        GyroInput gyro = default;

        protected void Update() {
            transform.rotation = gyro.rotation;
        }
    }
}