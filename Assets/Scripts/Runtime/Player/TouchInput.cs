using System;
using System.Collections;
using MizuKiri.Input;
using Slothsoft.UnityExtensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace MizuKiri {
    public class TouchInput : MonoBehaviour {
        public event Action<Vector2, double> onTouchStart;
        public event Action<Vector2, double> onTouchMove;
        public event Action<Vector2, double> onTouchStop;

        PlayerControls controls;

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

        bool isTouching = false;

        void HandleTouch(InputAction.CallbackContext obj) {
            var touch = obj.ReadValue<TouchState>();
            switch (touch.phase) {
                case UnityEngine.InputSystem.TouchPhase.Began:
                case UnityEngine.InputSystem.TouchPhase.Moved:
                case UnityEngine.InputSystem.TouchPhase.Stationary:
                    if (isTouching) {
                        onTouchMove?.Invoke(touch.position, obj.time);
                    } else {
                        isTouching = true;
                        onTouchStart?.Invoke(touch.position, obj.time);
                    }
                    break;
                case UnityEngine.InputSystem.TouchPhase.Ended:
                case UnityEngine.InputSystem.TouchPhase.Canceled:
                case UnityEngine.InputSystem.TouchPhase.None:
                    if (isTouching) {
                        isTouching = false;
                        onTouchStop?.Invoke(touch.position, obj.time);
                    }
                    break;
                default:
                    Debug.Log($"Unknown touch phase: {touch.phase}");
                    break;
            }
        }

#if UNITY_EDITOR
        [Header("Simulate touch")]
        [SerializeField]
        Vector2[] touchPositions = Array.Empty<Vector2>();

        IEnumerator SimulateTouch() {
            onTouchStart?.Invoke(touchPositions[0], Time.realtimeSinceStartupAsDouble);
            yield return null;
            for (int i = 1; i < touchPositions.Length - 1; i++) {
                onTouchMove?.Invoke(touchPositions[i], Time.realtimeSinceStartupAsDouble);
                yield return null;
            }
            onTouchMove?.Invoke(touchPositions[^1], Time.realtimeSinceStartupAsDouble);
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