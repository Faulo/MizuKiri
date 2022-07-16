using MizuKiri.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MizuKiri {
    public class TouchInput : MonoBehaviour {
        [field: SerializeField]
        public Vector2 position { get; private set; }

        PlayerControls controls;

        protected void OnEnable() {
            controls = new();
            controls.Player.Touch.started += HandleStart;
            controls.Player.Touch.performed += HandleStart;
            controls.Enable();
        }

        void HandleStart(InputAction.CallbackContext obj) {
            Debug.Log(obj);
            UpdateField(obj.ReadValue<Touch>());
        }

        protected void OnDisable() {
            if (controls != null) {
                controls.Disable();
                controls.Dispose();
                controls = null;
            }
        }

        protected void Update() {
        }

        void UpdateField(Touch touch) {
            position = touch.position;
        }
    }
}