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
        GameObject _pausePanel;
        [SerializeField]
        private void OnEnable()
        {
            GameManager.OnGameModeChange += this.OnGameModeChange;
            GameManager.OnGameStateChange += this.OnGameStateChange;
        }
        private void OnDisable()
        {

            GameManager.OnGameModeChange -= this.OnGameModeChange;
            GameManager.OnGameStateChange -= this.OnGameStateChange;

        }

        void Start()
        {
            this.OnGameModeChange(GameManager.instance.CurrentMode);
        }

        private void OnGameModeChange(GameMode newMode)
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
        
        private void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Running:
                    _pausePanel.SetActive(false);
                    this.OnGameModeChange(GameManager.instance.CurrentMode);
                    break;
                case GameState.Paused:
                    _pausePanel.SetActive(true);
                    _aRModePanel.SetActive(false);
                    _playModePanel.SetActive(false);

                    break;
                default:
                    break;
            }
        }
    }
}
