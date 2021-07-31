using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace KubeWorldAR
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField]
        GameObject _playMode;
        [SerializeField]
        GameObject _arMode;

        private void OnEnable()
        {
            GameManager.OnGameModeChange += OnModeChange;
        }
        private void OnDisable()
        {
            GameManager.OnGameModeChange -= OnModeChange;
        }

        private void Start()
        {
            OnModeChange(GameManager.instance.CurrentMode);
        }


        public void OnModeChange(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.EditMode:
                    _playMode.SetActive(false);
                    _arMode.SetActive(true);
                    break;
                case GameMode.PlayMode:
                    _playMode.SetActive(true);
                    _arMode.SetActive(false);
                    break;
                default:
                    break;
            }
        }

    }
}