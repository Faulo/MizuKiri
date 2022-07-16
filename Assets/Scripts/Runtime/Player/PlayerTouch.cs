using System.Collections.Generic;
using UnityEngine;

namespace MizuKiri.Player {
    public class PlayerTouch {

        public readonly Stone stone;

        readonly List<(Vector3 position, double time)> positions = new();

        public PlayerTouch(Stone stone) {
            this.stone = stone;

            stone.isKinematic = true;
        }

        public void AddPosition(Vector3 position) {
            stone.position = position;
            positions.Add((position, Time.realtimeSinceStartupAsDouble));
        }

        public void Launch(float smoothTime, float maxSpeed, float multiplier) {
            stone.isKinematic = false;

            stone.AddForce(multiplier * GetVelocity(smoothTime, maxSpeed));
        }

        Vector3 GetVelocity(float smoothTime, float maxSpeed) {
            var velocity = Vector3.zero;
            var previous = positions[0];
            for (int i = 1; i < positions.Count; i++) {
                var current = positions[i];
                Vector3.SmoothDamp(previous.position, current.position, ref velocity, smoothTime, maxSpeed, (float)(current.time - previous.time));
                previous = current;
            }
            return velocity;
        }

    }
}