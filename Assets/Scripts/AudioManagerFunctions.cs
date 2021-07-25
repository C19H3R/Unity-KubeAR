using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace KubeWorldAR
{
    public class AudioManagerFunctions : MonoBehaviour
    {

        [SerializeField]AudioMixer _master;
        [SerializeField] AudioMixer _music;

        public void ButtonSound()
        {
            AudioManager.instance.Play(SoundType.UiSound);
        }
    }
}