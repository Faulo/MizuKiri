using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace MizuKiri {
    public class DiveWater : ComponentFeature<Collider> {

        [SerializeField]
        float diveDrag = 10;
        [SerializeField]
        float diveVelocity = 0;
        [SerializeField]
        float destroyTimeout = 1;

        [Space]
        [SerializeField, Expandable]
        ParticleSystem splashPrefab = default;

        [Header("Events")]
        [SerializeField]
        UnityEvent<Vector3> onDive = new();

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
                stone.canDive = true;
            }
        }

        void ProcessStone(Stone stone) {
            if (stone.canDive) {
                stone.canDive = false;

                var position = observedComponent.ClosestPoint(stone.worldCenterOfMass);

                stone.AddForce(diveVelocity * stone.velocity3D);
                stone.drag = diveDrag;
                onDive.Invoke(stone.worldCenterOfMass);
            }
        }
    }
}