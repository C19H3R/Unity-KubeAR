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
        GameMode _gameMode;

        void Start()
        {
            _gameMode = PlayerPrefs.GetInt("MODE", 0) == 0 ?GameMode.EditMode:GameMode.PlayMode;
            if (_gameMode == GameMode.EditMode)
            {
                _aRModePanel.SetActive(true);
                _playModePanel.SetActive(false);
            }
            else
            {
                _aRModePanel.SetActive(false);
                _playModePanel.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
