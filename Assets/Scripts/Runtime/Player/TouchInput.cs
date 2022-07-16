using System;
using MizuKiri.Input;
using MizuKiri.Player;
using Slothsoft.UnityExtensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace MizuKiri {
    public class TouchInput : MonoBehaviour {
        public event Action<PlayerTouch> onTouch;

        PlayerControls controls;

        PlayerTouch currentTouch;

        protected void OnEnable() {
            controls = new();
            controls.Player.Touch.performed += HandleTouch;
            controls.Enable();
        }

        protected void OnDisable() {
            if (controls != null) {
                controls.Disable();
                controls.Dispose();
                controls = null;
            }
        }

        void HandleTouch(InputAction.CallbackContext obj) {
            var touch = obj.ReadValue<TouchState>();
            switch (touch.phase) {
                case UnityEngine.InputSystem.TouchPhase.Began:
                case UnityEngine.InputSystem.TouchPhase.Moved:
                case UnityEngine.InputSystem.TouchPhase.Stationary:
                    if (currentTouch == null) {
                        currentTouch = new();
                    }
                    AddTouch(touch);
                    break;
                case UnityEngine.InputSystem.TouchPhase.Ended:
                    if (currentTouch == null) {
                        break;
                    }
                    AddTouch(touch);
                    onTouch?.Invoke(currentTouch);
                    currentTouch = null;
                    break;
                default:
                    Debug.Log($"Unknown touch phase: {touch.phase}");
                    break;
            }
        }

        void AddTouch(in TouchState touch) {
            currentTouch.positions.Add(touch.position);
        }

#if UNITY_EDITOR
        [Header("Simulate touch")]
        [SerializeField]
        Vector2[] touchPositions = Array.Empty<Vector2>();

        void SimulateTouch() {
            var touch = new PlayerTouch();
            touch.positions.AddRange(touchPositions);
            onTouch?.Invoke(touch);
        }

        [CustomEditor(typeof(TouchInput))]
        class TouchInputEditor : RuntimeEditorTools<TouchInput> {
            protected override void DrawEditorTools() {
                DrawButton("Simulate Touch", target.SimulateTouch);
            }
        }
#endif
    }
}