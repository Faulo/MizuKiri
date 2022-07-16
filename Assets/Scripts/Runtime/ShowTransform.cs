using TMPro;
using UnityEngine;

namespace MizuKiri {
    public class ShowTransform : ComponentFeature<TMP_Text> {
        [SerializeField]
        Transform reference = default;

        protected void Update() {
            UpdateText();
        }

        protected void OnDrawGizmos() {
            UpdateText();
        }

        void UpdateText() {
            if (observedComponent && reference) {
                observedComponent.text = $"position: {reference.position}\nrotation: {reference.rotation.eulerAngles}\nscale: {reference.lossyScale}\n";
            }
        }
    }
}