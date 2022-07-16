using System;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Audio;

namespace MizuKiri {
    [CreateAssetMenu]
    public class AudioEffect : ScriptableObject {

        [SerializeField, Expandable]
        AudioSource prefab = default;

        [SerializeField, Expandable]
        AudioMixerGroup mixer = default;

        [Space]
        [SerializeField, Range(-10, 10)]
        float minPitch = 1;
        [SerializeField, Range(-10, 10)]
        float maxPitch = 1;
        [SerializeField, Range(0, 1)]
        float minVolume = 1;
        [SerializeField, Range(0, 1)]
        float maxVolume = 1;
        [SerializeField, Range(0, 60)]
        float offset = 0;

        [Space]
        [SerializeField]
        AudioClip[] clips = Array.Empty<AudioClip>();

        public void Play(Vector3 position) {
            var instance = Instantiate(prefab, position, Quaternion.identity);
            instance.clip = clips.RandomElement();
            instance.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            instance.volume = UnityEngine.Random.Range(minVolume, maxVolume);
            instance.time = offset;
            instance.outputAudioMixerGroup = mixer;
            instance.Play();
        }
    }
}