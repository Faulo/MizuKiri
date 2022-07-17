using System;
using MizuKiri.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MizuKiri.Player {
    public class GyroInput : MonoBehaviour {
        public event Action<Quaternion, Quaternion> onRotate;

        [SerializeField]
        Quaternion correctionRotation = Quaternion.identity;

        [SerializeField]
        Vector2 cameraSensitivity = Vector2.one;

        [SerializeField]
        float minXAngle = -90;
        [SerializeField]
        float maxXAngle = 90;
        [SerializeField]
        Vector2 look = Vector2.zero;

        PlayerControls controls;

        protected void OnEnable() {
            controls = new();
            if (GravitySensor.current != null) {
                InputSystem.EnableDevice(GravitySensor.current);
                controls.Gyro.Sensor.performed += HandleInput;
            }
            if (Mouse.current != null) {
                controls.Gyro.MouseClick.performed += HandleMouseClick;
                controls.Gyro.MouseDelta.performed += HandleMouseDelta;
            }
            controls.Gyro.Enable();
        }

        protected void OnDisable() {
            if (controls != null) {
                if (GravitySensor.current != null) {
                    InputSystem.DisableDevice(GravitySensor.current);
                }
                controls.Gyro.Disable();
                controls.Dispose();
                controls = null;
            }
        }

        bool isTouching = false;

        void HandleInput(InputAction.CallbackContext obj) {
            ProcessGravity(obj.ReadValue<Vector3>());
        }

        void ProcessGravity(Vector3 gravity) {
            var rotation = Quaternion.LookRotation(correctionRotation * -gravity).eulerAngles;
            look.x = rotation.y;
            look.y = rotation.x;

            UpdateLook();
        }

        void HandleMouseClick(InputAction.CallbackContext obj) {
            bool isPressed = obj.ReadValueAsButton();
            if (isPressed) {
                ContinueClick();
            } else {
                StopClick();
            }
        }

        void HandleMouseDelta(InputAction.CallbackContext obj) {
            if (isTouching) {
                ProcessDelta(obj.ReadValue<Vector2>());
            }
        }

        void ContinueClick() {
            isTouching = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void ProcessDelta(Vector2 delta) {
            look += delta * cameraSensitivity;
            look.y = Mathf.Clamp(look.y, minXAngle, maxXAngle);

            UpdateLook();
        }

        void StopClick() {
            isTouching = false;
            Cursor.lockState = CursorLockMode.None;
        }

        void UpdateLook() {
            onRotate?.Invoke(Quaternion.Euler(0, look.x, 0), Quaternion.Euler(look.y, 0, 0));
        }

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField]
        bool useDebugGravity = false;
        [SerializeField]
        Vector3 debugGravity = Vector3.zero;
        protected void Update() {
            if (useDebugGravity) {
                debugGravity.Normalize();
                ProcessGravity(debugGravity);
            }
        }
#endif
    }
}