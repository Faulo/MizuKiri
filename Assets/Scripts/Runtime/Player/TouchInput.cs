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
            controls.Touch.Touch.performed += HandleTouch;
            controls.Touch.MouseClick.performed += HandleTouchPress;
            controls.Touch.MousePosition.performed += HandleTouchPosition;
            controls.Touch.Enable();
        }

        protected void OnDisable() {
            if (controls != null) {
                controls.Touch.Disable();
                controls.Dispose();
                controls = null;
            }
        }

        bool isTouching = false;

        void HandleTouchPress(InputAction.CallbackContext obj) {
            bool isPressed = obj.ReadValueAsButton();
            if (isPressed) {
                ContinueClick(controls.Touch.MousePosition.ReadValue<Vector2>(), obj.time);
            } else {
                StopClick(controls.Touch.MousePosition.ReadValue<Vector2>(), obj.time);
            }
        }

        void HandleTouchPosition(InputAction.CallbackContext obj) {
            if (isTouching) {
                ContinueClick(obj.ReadValue<Vector2>(), obj.time);
            }
        }

        void HandleTouch(InputAction.CallbackContext obj) {
            var touch = obj.ReadValue<TouchState>();
            switch (touch.phase) {
                case UnityEngine.InputSystem.TouchPhase.Began:
                case UnityEngine.InputSystem.TouchPhase.Moved:
                case UnityEngine.InputSystem.TouchPhase.Stationary:
                    ContinueClick(touch.position, obj.time);
                    break;
                case UnityEngine.InputSystem.TouchPhase.Ended:
                case UnityEngine.InputSystem.TouchPhase.Canceled:
                case UnityEngine.InputSystem.TouchPhase.None:
                    StopClick(touch.position, obj.time);
                    break;
                default:
                    Debug.Log($"Unknown touch phase: {touch.phase}");
                    break;
            }
        }

        void ContinueClick(Vector2 position, double time) {
            if (isTouching) {
                onTouchMove?.Invoke(position, time);
            } else {
                isTouching = true;
                onTouchStart?.Invoke(position, time);
            }
        }

        void StopClick(Vector2 position, double time) {
            if (isTouching) {
                isTouching = false;
                onTouchStop?.Invoke(position, time);
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

            onTouchStop?.Invoke(touchPositions[^1], Time.realtimeSinceStartupAsDouble);
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