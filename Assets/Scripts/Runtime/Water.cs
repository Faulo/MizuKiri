using System;
using UnityEngine;

namespace MizuKiri {
    public class Water : ComponentFeature<Collider> {
        public event Action<Stone> onBounce;

        [SerializeField]
        float minimumSpeed = 10;
        [SerializeField]
        float repelMultiplier = 1;
        [SerializeField]
        float destroyTimeout = 1;

        protected void OnTriggerEnter(Collider collider) {
            if (collider.TryGetComponent<Stone>(out var stone)) {
                if (stone.velocity2D.magnitude > minimumSpeed) {
                    var position = observedComponent.ClosestPoint(stone.worldCenterOfMass);
                    stone.AddForceAtPosition(repelMultiplier * stone.velocity3D.y * Vector3.up, position);
                } else {
                    Destroy(stone.gameObject, destroyTimeout);
                }
            }
        }
    }
}