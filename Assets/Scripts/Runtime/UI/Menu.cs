using UnityEngine;

namespace MizuKiri.UI {
    [CreateAssetMenu]
    public class Menu : ScriptableObject {
        public void Quit() {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}