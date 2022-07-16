using Slothsoft.UnityExtensions;
using UnityEngine;

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

        public Vector3 position {
            get => attachedRigidbody.position;
            set => attachedRigidbody.position = value;
        }
        public bool isKinematic {
            get => attachedRigidbody.isKinematic;
            set => attachedRigidbody.isKinematic = value;
        }
        public Vector2 velocity2D => attachedRigidbody.velocity.SwizzleXZ();
        public Vector3 velocity3D => attachedRigidbody.velocity;
        public Vector3 worldCenterOfMass => attachedRigidbody.worldCenterOfMass;
        public void AddForce(Vector3 force) => attachedRigidbody.AddForce(force, ForceMode.VelocityChange);
        public void AddForceAtPosition(Vector3 force, Vector3 position) => attachedRigidbody.AddForceAtPosition(force, position, ForceMode.VelocityChange);

        PhysicMaterial physicsMaterial {
            get => attachedCollider.sharedMaterial;
            set => attachedCollider.sharedMaterial = value;
        }

        Material renderMaterial {
            get => attachedRenderer.sharedMaterial;
            set => attachedRenderer.sharedMaterial = value;
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
    }
}