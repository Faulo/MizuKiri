using System.Collections.Generic;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace MizuKiri {
    public class DiveWater : ComponentFeature<BoxCollider> {
        [SerializeField]
        float diveVelocity = 0;

        [Space]
        [SerializeField, Expandable]
        ParticleSystem splashPrefab = default;

        [Header("Events")]
        [SerializeField]
        UnityEvent<Vector3> onDive = new();

        public IEnumerable<Stone> containingStones => Physics
            .OverlapBox(transform.position + observedComponent.center, observedComponent.size / 2)
            .TrySelect<Collider, Stone>(c => c.TryGetComponent);

        protected void OnTriggerEnter(Collider collider) {
            if (collider.TryGetComponent<Stone>(out var stone)) {
                ProcessStone(stone);
            }
        }

        void ProcessStone(Stone stone) {
            if (stone.canDive) {
                stone.canDive = false;

                var position = observedComponent.ClosestPoint(stone.worldCenterOfMass);

                InstantiateParticles(stone, position);

                stone.AddForce(diveVelocity * stone.velocity3D);
                onDive.Invoke(position);
            }
        }

        void InstantiateParticles(Stone stone, Vector3 position) {
            var particles = Instantiate(splashPrefab, position, Quaternion.identity);
            var main = particles.main;
            main.startColor = stone.bounceColor;
        }
    }
}