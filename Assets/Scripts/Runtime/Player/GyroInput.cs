using System;
using MizuKiri.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MizuKiri.Player {
    public class GyroInput : MonoBehaviour {
        public event Action<Quaternion> onRotate;

        [SerializeField]
        Quaternion correctionRotation = Quaternion.identity;

        PlayerControls controls;

        protected void OnEnable() {
            controls = new();
            if (GravitySensor.current != null) {
                InputSystem.EnableDevice(GravitySensor.current);
                controls.Player.Gyro.started += HandleInput;
                controls.Player.Gyro.performed += HandleInput;
                controls.Player.Gyro.canceled += HandleInput;
            }
            controls.Enable();
        }

        protected void OnDisable() {
            if (controls != null) {
                if (GravitySensor.current != null) {
                    InputSystem.DisableDevice(GravitySensor.current);
                }
                controls.Disable();
                controls.Dispose();
                controls = null;
            }
        }

        void HandleInput(InputAction.CallbackContext obj) {
            Debug.Log(obj.ReadValue<Vector3>());
        }

        [Space]
        [SerializeField]
        Vector3 debugGravity = Vector3.zero;
        protected void Update() {
            if (GravitySensor.current == null) {
                debugGravity.Normalize();
                ProcessGravity(debugGravity);
            }
        }

        void ProcessGravity(Vector3 gravity) {
            var rotation = Quaternion.LookRotation(correctionRotation * -gravity);
            onRotate?.Invoke(rotation);
        }
    }
}