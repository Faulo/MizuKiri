using System.Collections;
using UnityEngine;

namespace MizuKiri {
    public class AudioAutoDestroy : ComponentFeature<AudioSource> {
        protected IEnumerator Start() {
            yield return null;
            yield return new WaitWhile(() => observedComponent.isPlaying);
            Destroy(gameObject);
        }
    }
}