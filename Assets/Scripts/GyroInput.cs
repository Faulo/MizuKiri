using UnityEngine;
using UnityEngine.InputSystem;

namespace MizuKiri {
    public class GyroInput : MonoBehaviour {
        [field: SerializeField]
        public Quaternion rotation { get; private set; }

        protected void OnEnable() {
            if (AttitudeSensor.current != null) {
                InputSystem.EnableDevice(AttitudeSensor.current);
            }
        }
        protected void OnDisable() {
            if (AttitudeSensor.current != null) {
                InputSystem.DisableDevice(AttitudeSensor.current);
            }
        }

        protected void Update() {
            if (AttitudeSensor.current != null) {
                rotation = GetGyroRotation();
            }
        }

        Quaternion GetGyroRotation() {
            var rotation = AttitudeSensor.current.attitude.ReadValue();
            return new Quaternion(rotation.x, rotation.y, -rotation.z, -rotation.w);
        }
    }
}