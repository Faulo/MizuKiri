using System.Collections;
using MizuKiri.Player;
using TMPro;
using UnityEngine;

namespace MizuKiri.UI {
    public class ThrowPanel : ComponentFeature<TMP_Text> {
        enum PanelType {
            Current,
            Highscore
        }
        [SerializeField]
        PlayerController player = default;
        [Space]
        [SerializeField]
        PanelType type = PanelType.Current;

        protected override void SetUpComponents() {
            base.SetUpComponents();
            if (!player) {
                player = FindObjectOfType<PlayerController>(true);
            }
        }

        string template;
        protected override void Awake() {
            base.Awake();
            template = observedComponent.text;
            UpdateText();
        }

        protected void OnEnable() {
            player.onStartThrow += HandleThrow;
        }

        protected void OnDisable() {
            player.onStartThrow -= HandleThrow;
        }

        void HandleThrow(StoneThrow stoneThrow) {
            StopAllCoroutines();
            StartCoroutine(WatchThrow(stoneThrow));
        }
        IEnumerator WatchThrow(StoneThrow stoneThrow) {
            while (stoneThrow.stone) {
                UpdateStats(stoneThrow.stone.bounces, stoneThrow.stone.travelDistance);
                yield return null;
            }
        }

        int bounces;
        float travelDistance;

        void UpdateStats(int bounces, float travelDistance) {
            switch (type) {
                case PanelType.Current:
                    this.bounces = bounces;
                    this.travelDistance = travelDistance;
                    break;
                case PanelType.Highscore:
                    if (bounces > this.bounces) {
                        this.bounces = bounces;
                    }
                    if (travelDistance > this.travelDistance) {
                        this.travelDistance = travelDistance;
                    }
                    break;
            }
            UpdateText();
        }

        void UpdateText() {
            if (Mathf.Approximately(travelDistance, 0)) {
                observedComponent.text = "";
            } else {
                observedComponent.text = template
                    .Replace("$skips", bounces.ToString())
                    .Replace("$skim", Mathf.RoundToInt(travelDistance).ToString());
            }
        }

    }
}