using System.Collections.Generic;
using UnityEngine;

namespace MizuKiri.Player {
    public class PlayerTouch {

        readonly Stone stone;

        readonly List<(Vector3 position, double time)> positions = new();

        public PlayerTouch(Stone stone) {
            this.stone = stone;

            stone.isKinematic = true;
        }

        public void AddPosition(Vector3 position) {
            stone.position = position;
            positions.Add((position, Time.realtimeSinceStartupAsDouble));
        }

        public void Launch(float smoothTime, float multiplier) {
            stone.isKinematic = false;

            stone.AddForce(multiplier * GetVelocity(smoothTime));
        }

        Vector3 GetVelocity(float smoothTime) {
            var velocity = Vector3.zero;
            var acceleration = Vector3.zero;
            for (int i = 1; i < positions.Count; i++) {
                var target = (positions[i].position - positions[i - 1].position) / (float)(positions[i].time - positions[i - 1].time);
                velocity = Vector3.SmoothDamp(velocity, target, ref acceleration, smoothTime, float.PositiveInfinity, Time.fixedDeltaTime);
            }
            return velocity;
        }

    }
}