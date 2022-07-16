using UnityEngine;

namespace MizuKiri.Player {
    public class StoneThrow {

        public readonly Stone stone;

        Vector3 currentPosition {
            get => stone.position;
            set => stone.position = value;
        }
        public Vector3 targetPosition;

        readonly float smoothTime;
        readonly float maxSpeed;

        Vector3 velocity;

        public StoneThrow(Stone stone, float smoothTime, float maxSpeed) {
            this.stone = stone;
            this.smoothTime = smoothTime;
            this.maxSpeed = maxSpeed;

            stone.freezePosition = true;

            targetPosition = currentPosition;
        }

        public void FixedUpdate(float deltaTime) {
            currentPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime, maxSpeed, deltaTime);
        }

        public void Launch(Vector3 position, float multiplier) {
            currentPosition = targetPosition = position;
            stone.freezePosition = false;

            stone.AddForce(multiplier * velocity);
        }
    }
}