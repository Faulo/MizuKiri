using System;
using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace MizuKiri {
    public class Stone : ComponentFeature<Rigidbody> {
        public Rigidbody attachedRigidbody => observedComponent;

        public Mesh mesh {
            get => m_mesh;
            set {
                m_mesh = value;
                attachedCollider.sharedMesh = value;
                attachedFilter.sharedMesh = value;
            }
        }

        public bool freezePosition {
            get => attachedRigidbody.constraints == RigidbodyConstraints.FreezePosition;
            set => attachedRigidbody.constraints = value
                ? RigidbodyConstraints.FreezePosition
                : RigidbodyConstraints.None;
        }

        [SerializeField]
        Mesh m_mesh = default;

        public (Material rendering, PhysicMaterial physics) materials {
            get => (renderMaterial, physicsMaterial);
            set => (renderMaterial, physicsMaterial) = value;
        }

        [SerializeField]
        MeshCollider attachedCollider = default;
        [SerializeField]
        MeshRenderer attachedRenderer = default;
        [SerializeField]
        MeshFilter attachedFilter = default;

        [Header("Bounciness")]
        [SerializeField, Range(0, 100)]
        float speedThreshold = 10;
        [SerializeField, Slothsoft.UnityExtensions.Layer]
        int bounceLayer = 0;
        [SerializeField, Slothsoft.UnityExtensions.Layer]
        int diveLayer = 0;
        [SerializeField, Slothsoft.UnityExtensions.Layer]
        int throwLayer = 0;

        public Vector3 position {
            get => freezePosition
                ? transform.position
                : attachedRigidbody.position;
            set {
                if (freezePosition) {
                    transform.position = value;
                } else {
                    attachedRigidbody.position = value;
                }
            }
        }
        public Vector3 forward => transform.forward;
        public Vector2 velocity2D => attachedRigidbody.velocity.SwizzleXZ();
        public Vector3 velocity3D => attachedRigidbody.velocity;
        public Vector3 worldCenterOfMass => attachedRigidbody.worldCenterOfMass;
        public void AddForce(Vector3 force) => attachedRigidbody.AddForce(force, ForceMode.VelocityChange);
        public void AddTorque(Vector3 torque) => attachedRigidbody.AddRelativeTorque(torque, ForceMode.VelocityChange);
        public void AddForceAtPosition(Vector3 force, Vector3 position) => attachedRigidbody.AddForceAtPosition(force, position, ForceMode.VelocityChange);

        PhysicMaterial physicsMaterial {
            get => attachedCollider.sharedMaterial;
            set => attachedCollider.sharedMaterial = value;
        }

        Material renderMaterial {
            get => attachedRenderer.sharedMaterial;
            set => attachedRenderer.sharedMaterial = value;
        }

        [Header("Debug")]
        [SerializeField, ReadOnly]
        public int bounces = 0;
        [SerializeField, ReadOnly]
        public Vector3 travelStart = Vector3.zero;
        [SerializeField, ReadOnly]
        public Vector3 travelStop = Vector3.zero;
        public float travelDistance => freezePosition
            ? 0
            : Vector2.Distance(travelStart.SwizzleXZ(), travelStop.SwizzleXZ());
        [NonSerialized]
        public bool canBounce = false;
        [NonSerialized]
        public bool canDive = false;

        [SerializeField]
        Color m_bounceColor = Color.blue;
        [SerializeField]
        Vector3 bounceColorMultiplier = Vector3.zero;
        public MinMaxGradient bounceColor {
            get {
                var startColor = m_bounceColor;

                Color.RGBToHSV(startColor, out float h, out float s, out float v);

                h += bounceColorMultiplier.x * bounces;
                while (h > 1) {
                    h--;
                }
                s += bounceColorMultiplier.x * bounces;
                v += bounceColorMultiplier.x * bounces;

                var stopColor = Color.HSVToRGB(h, s, v);

                return new MinMaxGradient(startColor, stopColor);
            }
        }

        protected override void SetUpComponents() {
            base.SetUpComponents();
            if (!attachedCollider) {
                transform.TryGetComponentInChildren(out attachedCollider);
            }
            if (!attachedRenderer) {
                transform.TryGetComponentInChildren(out attachedRenderer);
            }
            if (!attachedFilter) {
                transform.TryGetComponentInChildren(out attachedFilter);
            }
        }

        protected void FixedUpdate() {
            if (position.y < 0 || (bounces > 0 && attachedRigidbody.IsSleeping())) {
                Destroy(gameObject);
                return;
            }
            gameObject.layer = freezePosition
                ? throwLayer
                : velocity2D.magnitude > speedThreshold
                    ? bounceLayer
                    : diveLayer;
            if (canDive) {
                travelStop = position;
            }
        }
    }
}