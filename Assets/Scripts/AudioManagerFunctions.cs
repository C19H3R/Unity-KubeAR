using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



namespace KubeWorldAR
{
    public class AudioManagerFunctions : MonoBehaviour
    {

        [SerializeField] AudioMixer _master;
        [SerializeField] AudioMixer _music;

        [SerializeField] Slider _musicSlider;
        [SerializeField] Slider _masterSlider;


        [HideInInspector]
        float masterLevel, musicLevel;
        public void ButtonSound()
        {
            AudioManager.instance.Play(SoundType.UiSound);
        }

        public void SetMasterVolume(float level)
        {
            masterLevel = Mathf.Log10(level) * 20;
            _master.SetFloat("MasterVolume", masterLevel);

            Debug.Log("GG");
            if (_musicSlider.value > level)
                _musicSlider.value = level;
        }
        public void SetMusicVolume(float level)
        {
            musicLevel = Mathf.Log10(level) * 20;
            _music.SetFloat("MusicVolume", musicLevel);

            if (_masterSlider.value < level)
                _masterSlider.value = level;

        }

        public void SetVolumeInGame(float level)
        {
            musicLevel = Mathf.Log10(level) * 20;
            _music.SetFloat("MusicVolume", musicLevel);
            masterLevel = Mathf.Log10(level) * 20;
            _master.SetFloat("MasterVolume", masterLevel);

        }
    }
}