using System.Collections;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri {
    [CreateAssetMenu]
    public class PhysicsStepper : ScriptableObject {
#if UNITY_EDITOR
        [SerializeField]
        float deltaTimePerStep = 1f/50;
        [SerializeField]
        int framesPerStep = 10;
        [SerializeField]
        int totalFramesToSimulate = 1000;

        IEnumerator Simulate() {
            Physics.autoSimulation = false;
            for (int i = 0; i < totalFramesToSimulate; i++) {
                Physics.Simulate(deltaTimePerStep);
                if (i % framesPerStep == 0) {
                    yield return null;
                }
            }
            Physics.autoSimulation = true;
        }

        [UnityEditor.CustomEditor(typeof(PhysicsStepper))]
        class PhysicsStepperEditor : RuntimeEditorTools<PhysicsStepper> {
            protected override void DrawEditorTools() {
                DrawButton("Simulate!", target.Simulate);
            }
        }
#endif
    }
}