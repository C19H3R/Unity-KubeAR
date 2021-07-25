using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KubeWorldAR
{
    public class SliderUiChanger : MonoBehaviour
    {

        [SerializeField]
        Image _targetSliderKnob;
        [SerializeField]
        List<Sprite> _knobSprites;

        public void OnSpeakerValueChange(float value)
        {
            if (value == 0)
            {
                _targetSliderKnob.sprite = _knobSprites[0];
            }
            else if (value < 0.6f)
            {
                _targetSliderKnob.sprite = _knobSprites[1];
            }
            else if (value >= 0.6f)
            {
                _targetSliderKnob.sprite = _knobSprites[2];
            }
        }
        public void OnMusicValueChange(float value)
        {
            if (value == 0)
            {
                _targetSliderKnob.sprite = _knobSprites[0];
            }
            else
            {
                _targetSliderKnob.sprite = _knobSprites[1];
            }
        }
    }
}