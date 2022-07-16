using MizuKiri.Player;
using TMPro;
using UnityEngine;

namespace MizuKiri {
    public class ShowStone : ComponentFeature<TMP_Text> {
        [SerializeField]
        PlayerController player = default;

        [SerializeField]
        Stone currentStone = default;

        protected void OnEnable() {
            player.onStartThrow += HandleThrow;
        }
        protected void OnDisable() {
            player.onStartThrow -= HandleThrow;
        }

        void HandleThrow(PlayerTouch obj) {
            currentStone = obj.stone;
        }

        protected void Update() {
            UpdateText();
        }

        void UpdateText() {
            if (observedComponent && currentStone) {
                observedComponent.text = $"position: {currentStone.position}\nvelocity: {currentStone.velocity3D}";
            } else {
                observedComponent.text = "";
            }
        }
    }
}