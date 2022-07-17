using System;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri.Player {
    public class PlayerController : MonoBehaviour {
        public event Action<StoneThrow> onStartThrow;

        [SerializeField]
        TouchInput touch = default;

        [SerializeField]
        GyroInput gyro = default;

        [SerializeField, Expandable]
        Camera attachedCamera = default;

        [Space]
        [SerializeField, Expandable]
        StoneFactory factory = default;

        [Header("Pointing")]
        [SerializeField]
        AnimationCurve cameraDistanceOverY = AnimationCurve.Linear(0, 0, 1, 10);

        [Header("Throwing")]
        [SerializeField]
        float throwSpeedSmoothing = 0.1f;
        [SerializeField]
        float throwSpeedMaximum = 1000;
        [SerializeField]
        float throwSpeedMultiplier = 10;

        StoneThrow currentStone;

        protected void OnValidate() {
            if (!touch) {
                transform.TryGetComponentInChildren(out touch);
            }
            if (!gyro) {
                transform.TryGetComponentInChildren(out gyro);
            }
            if (!attachedCamera) {
                attachedCamera = FindObjectOfType<Camera>();
            }
        }

        protected void OnEnable() {
            touch.onTouchStart += HandleTouchStart;
            touch.onTouchMove += HandleTouchMove;
            touch.onTouchStop += HandleTouchStop;
            gyro.onRotate += HandleRotate;
        }

        protected void OnDisable() {
            touch.onTouchStart -= HandleTouchStart;
            touch.onTouchMove -= HandleTouchMove;
            touch.onTouchStop -= HandleTouchStop;
            gyro.onRotate -= HandleRotate;
        }

        protected void FixedUpdate() {
            currentStone?.FixedUpdate(Time.deltaTime);
        }

        void HandleTouchStart(Vector2 position, double time) {
            if (currentStone == null) {
                currentStone = new StoneThrow(factory.InstantiateStone(TranslatePosition(position)), throwSpeedSmoothing, throwSpeedMaximum);
                onStartThrow?.Invoke(currentStone);
            }
        }
        void HandleTouchMove(Vector2 position, double time) {
            if (currentStone != null) {
                currentStone.targetPosition = TranslatePosition(position);
            }
        }
        void HandleTouchStop(Vector2 position, double time) {
            if (currentStone != null) {
                currentStone.Launch(TranslatePosition(position), throwSpeedMultiplier);
                currentStone = null;
            }
        }

        Vector3 TranslatePosition(Vector2 screenPosition) {
            return attachedCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, cameraDistanceOverY.Evaluate(screenPosition.y / Screen.height)));
        }

        void HandleRotate(Quaternion rotation) {
            attachedCamera.transform.localRotation = rotation;
        }
    }
}