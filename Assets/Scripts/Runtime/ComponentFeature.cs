using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri {
    public class ComponentFeature<T> : MonoBehaviour where T : Component {
        public T observedComponent => m_observedComponent;

        [SerializeField, Expandable]
        T m_observedComponent = default;

        protected virtual void Awake() {
            SetUpComponents();
        }
        protected virtual void OnValidate() {
            SetUpComponents();
        }
        protected virtual void SetUpComponents() {
            if (!m_observedComponent) {
                TryGetComponent(out m_observedComponent);
                if (!m_observedComponent) {
                    transform.TryGetComponentInParent(out m_observedComponent);
                    if (!m_observedComponent) {
                        transform.TryGetComponentInChildren(out m_observedComponent);
                    }
                }
            }
        }
    }
}