using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri.Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField]
        TouchInput touch = default;

        [SerializeField, Expandable]
        StoneSpawner spawner = default;

        [SerializeField, Expandable]
        Camera attachedCamera = default;

        [SerializeField]
        AnimationCurve cameraDistanceOverY = AnimationCurve.Linear(0, 0, 1, 10);

        [SerializeField]
        float throwSpeed = 10;

        protected void OnValidate() {
            if (!touch) {
                transform.TryGetComponentInChildren(out touch);
            }
        }

        protected void OnEnable() {
            touch.onTouch += HandleTouch;
        }

        protected void OnDisable() {
            touch.onTouch -= HandleTouch;
        }

        void HandleTouch(PlayerTouch touch) {
            var stone = spawner.SpawnStone();
            var position2D = touch.positions[^1];
            var position3D = new Vector3(position2D.x, position2D.y, cameraDistanceOverY.Evaluate(position2D.y / Screen.height));
            stone.position = attachedCamera.ScreenToWorldPoint(position3D);
            stone.AddForce(spawner.transform.forward * throwSpeed);
        }
    }
}