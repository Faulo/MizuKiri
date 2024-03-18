using System.Collections;
using System.Linq;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace MizuKiri {
    [CreateAssetMenu]
    public class PhysicsStepper : ScriptableObject {
#if UNITY_EDITOR
        [SerializeField]
        float deltaTimePerStep = 1f / 50;
        [SerializeField]
        int framesPerStep = 10;
        [SerializeField]
        int totalFramesToSimulate = 1000;

        public IEnumerator Simulate() {
            for (int i = 0; i < totalFramesToSimulate; i++) {
                Physics.simulationMode = SimulationMode.Script;
                Physics.Simulate(deltaTimePerStep);
                if (i % framesPerStep == 0) {
                    yield return null;
                }
            }

            yield return null;
            Physics.simulationMode = SimulationMode.FixedUpdate;
        }

        [SerializeField]
        float rigidbodySettleTime = 5;

        public IEnumerator SettleRigidbodies() {
            Debug.Log("Settling rigidbodies...");
            var rigidbodies = FindObjectsOfType<Rigidbody>();

            for (int i = 0; i < rigidbodies.Length; i++) {
                rigidbodies[i].WakeUp();
            }

            float time = Time.realtimeSinceStartup + rigidbodySettleTime;

            do {
                Physics.simulationMode = SimulationMode.Script;
                Physics.Simulate(deltaTimePerStep);
                yield return null;
            } while (Time.realtimeSinceStartup < time && rigidbodies.Any(rigidbody => !rigidbody.IsSleeping()));

            Debug.Log("...done!");
        }

        [UnityEditor.CustomEditor(typeof(PhysicsStepper))]
        class PhysicsStepperEditor : RuntimeEditorTools<PhysicsStepper> {
            protected override void DrawEditorTools() {
                DrawButton("Simulate", target.Simulate);
                DrawButton("Settle rigidbodies", target.SettleRigidbodies);
            }
        }
#endif
    }
}