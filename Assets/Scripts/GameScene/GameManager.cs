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
    public enum GameState
    {
        Running=0,
        Paused=1
    }




    public class GameManager : MonoBehaviour
    {
        #region Singleton
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
        public static event Action<GameState> OnGameStateChange;
        [SerializeField]
        GameMode _currentMode;
        
        public GameMode CurrentMode
        {
            get { return _currentMode; }
        }
        
        [SerializeField]
        GameState _currentState;

        public GameState CurrentState
        {
            get { return _currentState; }
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

        public void ChangeGameState(GameState newState)
        {
            _currentState = newState;

            switch (newState)
            {
                case GameState.Running:
                    break;
                case GameState.Paused:
                    break;
                default:
                    break;
            }

            OnGameStateChange?.Invoke(newState);
        }
    }
}