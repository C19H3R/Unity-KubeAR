using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KubeWorldAR
{

    public class GameCanvasManager : MonoBehaviour
    {
        [SerializeField]
        GameObject _aRModePanel;
        [SerializeField]
        GameObject _playModePanel;
        [SerializeField]
        private void OnEnable()
        {
            GameManager.OnGameModeChange += SwitchGameModePanel;
        }
        private void OnDisable()
        {
            GameManager.OnGameModeChange -= SwitchGameModePanel;
        }

        void Start()
        {
            SwitchGameModePanel(GameManager.instance.CurrentMode);
        }

        private void SwitchGameModePanel(GameMode newMode)
        {
            switch (newMode)
            {
                case GameMode.EditMode:
                    _aRModePanel.SetActive(true);
                    _playModePanel.SetActive(false);
                    break;
                case GameMode.PlayMode:
                    _aRModePanel.SetActive(false);
                    _playModePanel.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
