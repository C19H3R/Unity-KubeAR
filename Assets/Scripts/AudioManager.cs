using UnityEngine;
using UnityEngine.Audio;


namespace KubeWorldAR
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0,1)]
        public float volume;
        [Range(0.1f, 3)]
        public float pitch;
        public AudioSource source;
    }

    public class AudioManager : MonoBehaviour
    {

        [SerializeField]
        Sound[] _sounds;

        private void Awake()
        {
            foreach ( Sound s in _sounds)
            {
                s.source =  gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}