using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}