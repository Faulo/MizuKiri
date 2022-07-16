using MizuKiri.Player;
using UnityEngine;

namespace MizuKiri {
    public class UpdateTransformRotation : ComponentFeature<Transform> {
        [SerializeField]
        GyroInput gyro = default;

        protected void Update() {
            observedComponent.rotation = gyro.rotation;
        }
    }
}