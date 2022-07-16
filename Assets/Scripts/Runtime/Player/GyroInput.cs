using MizuKiri.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MizuKiri.Player {
    public class GyroInput : MonoBehaviour {
        [field: SerializeField]
        public Quaternion rotation { get; private set; }

        PlayerControls controls;

        protected void OnEnable() {
            controls = new();
            controls.Player.Gyro.started += HandleInput;
            controls.Player.Gyro.performed += HandleInput;
            controls.Player.Gyro.canceled += HandleInput;
            controls.Enable();
        }

        protected void OnDisable() {
            if (controls != null) {
                controls.Disable();
                controls.Dispose();
                controls = null;
            }
        }

        void HandleInput(InputAction.CallbackContext obj) {
            Debug.Log(obj);
        }

        protected void Update() {
            if (controls.Player.Gyro.enabled) {
                rotation = GetGyroRotation();
            }
        }

        Quaternion GetGyroRotation() {
            var rotation = controls.Player.Gyro.ReadValue<Quaternion>();
            return new Quaternion(rotation.x, rotation.y, -rotation.z, -rotation.w);
        }
    }
}