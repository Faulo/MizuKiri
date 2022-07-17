using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace MizuKiri {
    public class DiveWater : ComponentFeature<Collider> {

        [SerializeField]
        float diveDrag = 10;
        [SerializeField]
        float diveVelocity = 0;

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