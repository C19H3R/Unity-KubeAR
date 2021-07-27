using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KubeWorldAR
{
    public class GameManagerFunctions : MonoBehaviour
    {
        public void PlayMode()
        {
            GameManager.instance.SwitchGameMode(GameMode.PlayMode);
        }
        public void EditMode()
        {
            GameManager.instance.SwitchGameMode(GameMode.EditMode);
        }
        public void PauseGame()
        {
            GameManager.instance.ChangeGameState(GameState.Paused);
        }
        public void ResumeGame()
        {
            GameManager.instance.ChangeGameState(GameState.Running);
        }
        public void QuitGame()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

    }
}