using UnityEngine;

namespace MizuKiri {
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