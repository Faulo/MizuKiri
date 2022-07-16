using UnityEngine;
using UnityEngine.Events;

namespace MizuKiri {
    public class Water : ComponentFeature<Collider> {
        [SerializeField]
        UnityEvent<Vector3> onBounce = new();
        [SerializeField]
        UnityEvent<Vector3> onDive = new();

        [SerializeField]
        float minimumHorizontalSpeed = 10;
        [SerializeField]
        float minimumVerticalSpeed = 1;
        [SerializeField]
        float repelMultiplier = 1;
        [SerializeField]
        float repelMinimum = 1;
        [SerializeField]
        float destroyTimeout = 1;

        protected void OnTriggerEnter(Collider collider) {
            if (collider.TryGetComponent<Stone>(out var stone)) {
                var position = observedComponent.ClosestPoint(stone.worldCenterOfMass);
                if (stone.velocity2D.magnitude > minimumHorizontalSpeed && Mathf.Abs(stone.velocity3D.y) > minimumVerticalSpeed) {
                    onBounce.Invoke(position);
                    stone.AddForceAtPosition(Mathf.Max(repelMultiplier * stone.velocity3D.y, repelMinimum) * Vector3.up, position);
                } else {
                    onDive.Invoke(position);
                    Destroy(stone.gameObject, destroyTimeout);
                }
            }
        }
    }
}