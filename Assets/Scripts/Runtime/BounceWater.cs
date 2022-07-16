using UnityEngine;
using UnityEngine.Events;

namespace MizuKiri {
    public class BounceWater : ComponentFeature<Collider> {
        [SerializeField]
        float repelMultiplier = 1;
        [SerializeField]
        float repelForward = 1;

        [Header("Events")]
        [SerializeField]
        UnityEvent<Vector3> onBounce = new();

        protected void OnTriggerEnter(Collider collider) {
            if (collider.TryGetComponent<Stone>(out var stone)) {
                ProcessStone(stone);
            }
        }
        protected void OnTriggerStay(Collider collider) {
            if (collider.TryGetComponent<Stone>(out var stone)) {
                ProcessStone(stone);
            }
        }
        protected void OnTriggerExit(Collider collider) {
            if (collider.TryGetComponent<Stone>(out var stone)) {
                stone.canBounce = true;
            }
        }

        void ProcessStone(Stone stone) {
            if (stone.canBounce) {
                stone.canBounce = false;

                stone.bounces++;

                var position = observedComponent.ClosestPoint(stone.worldCenterOfMass);

                onBounce.Invoke(position);
                stone.AddForceAtPosition(repelForward * stone.forward, position);
                stone.AddForceAtPosition(repelMultiplier * stone.velocity3D.y * Vector3.up, position);
                stone.AddTorque(new Vector3(0, stone.velocity2D.magnitude, 0));
            }
        }
    }
}