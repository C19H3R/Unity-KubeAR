using System;
using UnityEngine;
using UnityEngine.Audio;

namespace KubeWorldAR
{
    [System.Serializable]
    public class Sound
    {
        public SoundType type;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = .75f;
        [Range(0f, 1f)]
        public float volumeVariance = .1f;

        [Range(.1f, 3f)]
        public float pitch = 1f;
        [Range(0f, 1f)]
        public float pitchVariance = .1f;

        public bool loop = false;

        public AudioMixerGroup mixerGroup;

        [HideInInspector]
        public AudioSource source;
    }
    [System.Serializable]
    public enum SoundType
    {
        ThemeMusic = 0,
        UiSound = 1,
    }




    public class AudioManager : MonoBehaviour
    {

        [SerializeField]
        Sound[] _sounds;

        public static AudioManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            foreach (Sound s in _sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.mixerGroup;

            }
        }

        private void Start()
        {
            AudioManager.instance.Play(SoundType.ThemeMusic);
        }

        public void Play(SoundType sound)
        {
            Sound s = Array.Find(_sounds, item => item.type == sound);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            s.source.Play();
        }
        public void Play(int sound)
        {
            Sound s = Array.Find(_sounds, item => item.type == (SoundType)sound);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            s.source.Play();
        }
    }
}