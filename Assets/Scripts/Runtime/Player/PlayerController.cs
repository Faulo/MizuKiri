using System;
using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri.Player {
    public class PlayerController : MonoBehaviour {
        public event Action<StoneThrow> onStartThrow;

        [Header("Input")]
        [SerializeField]
        TouchInput touch = default;
        [SerializeField]
        GyroInput gyro = default;

        [Header("Camera")]
        [SerializeField, Expandable]
        Camera attachedCamera = default;
        [SerializeField, Range(0, 10)]
        float rotationSmoothing = 1;
        [SerializeField, Expandable]
        Transform attachedCameraBody = default;
        [SerializeField, ReadOnly]
        Quaternion targetBodyRotation = Quaternion.identity;
        [SerializeField, ReadOnly]
        Vector3 bodyRotationVelocity = Vector3.zero;
        [SerializeField, Expandable]
        Transform attachedCameraHead = default;
        [SerializeField, ReadOnly]
        Quaternion targetHeadRotation = Quaternion.identity;
        [SerializeField, ReadOnly]
        Vector3 headRotationVelocity = Vector3.zero;

        [Header("Pointing")]
        [SerializeField, Expandable]
        StoneFactory factory = default;
        [SerializeField]
        bool useStoneStorage = true;
        [SerializeField]
        float maxStorageDistance = 1000;
        [SerializeField]
        LayerMask storageLayers = default;
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
            targetBodyRotation = attachedCameraBody.localRotation;
            targetHeadRotation = attachedCameraHead.localRotation;
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

        protected void Update() {
            attachedCameraBody.localRotation = SmoothDampQuaternion(attachedCameraBody.localRotation, targetBodyRotation, ref bodyRotationVelocity, rotationSmoothing);
            attachedCameraHead.localRotation = SmoothDampQuaternion(attachedCameraHead.localRotation, targetHeadRotation, ref headRotationVelocity, rotationSmoothing);
        }

        protected void FixedUpdate() {
            currentStone?.FixedUpdate(Time.deltaTime);
        }

        void HandleTouchStart(Vector2 position, double time) {
            if (currentStone == null) {
                var position3D = TranslatePosition(position);
                var stone = useStoneStorage
                    ? FindStoneOnScreen(position)
                    : factory.InstantiateStone(position3D);
                if (stone) {
                    currentStone = new StoneThrow(stone, throwSpeedSmoothing, throwSpeedMaximum) {
                        targetPosition = position3D
                    };
                    onStartThrow?.Invoke(currentStone);
                }
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

        [SerializeField]
        float groundOffset = 1;

        [SerializeField]
        LayerMask groundLayers = default;

        Vector3 TranslatePosition(Vector2 screenPosition2D) {
            float maxDistance = cameraDistanceOverY.Evaluate(screenPosition2D.y / Screen.height);
            var screenPosition3D = new Vector3(screenPosition2D.x, screenPosition2D.y, cameraDistanceOverY.Evaluate(screenPosition2D.y / Screen.height));
            var ray = attachedCamera.ScreenPointToRay(screenPosition3D);
            if (Physics.Raycast(ray, out var info, maxDistance + groundOffset, groundLayers, QueryTriggerInteraction.Collide)) {
                maxDistance = info.distance - groundOffset;
            }
            return ray.GetPoint(maxDistance);
        }

        Stone FindStoneOnScreen(Vector2 screenPosition2D) {
            var ray = attachedCamera.ScreenPointToRay(screenPosition2D.SwizzleXY());
            if (Physics.Raycast(ray, out var info, maxStorageDistance, storageLayers, QueryTriggerInteraction.Ignore)) {
                return info.collider.GetComponent<Stone>();
            }
            return null;
        }

        void HandleRotate(Quaternion bodyRotation, Quaternion headRotation) {
            targetBodyRotation = bodyRotation;
            targetHeadRotation = headRotation;
        }

        static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime) {
            var c = current.eulerAngles;
            var t = target.eulerAngles;
            return Quaternion.Euler(
              Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
              Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
              Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
            );
        }
    }
}