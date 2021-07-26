using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KubeWorldAR
{

    [System.Serializable]
    public enum GameMode
    {
        EditMode = 0,
        PlayMode = 1,
    }
    



    public class GameManager : MonoBehaviour
    {
        #region Singelton
        public static GameManager instance;

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
        }
        #endregion

        public static event Action<GameMode> OnGameModeChange;
        [SerializeField]
        GameMode _currentMode;
        public GameMode CurrentMode
        {
            get { return _currentMode; }
        }
        public void SwitchGameMode(GameMode newMode)
        {
            _currentMode = newMode;

            switch (newMode)
            {
                case GameMode.EditMode:
                    break;
                case GameMode.PlayMode:
                    break;
                default:
                    break;
            }

            OnGameModeChange?.Invoke(newMode);
        }
    }
}