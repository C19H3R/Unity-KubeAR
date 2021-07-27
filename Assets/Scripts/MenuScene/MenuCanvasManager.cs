using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace KubeWorldAR {

    [System.Serializable]
    public enum PanelType
    {
        MainMenu = 0,
        Settings = 1,
        Controls = 2,
        HowToPlay = 3,
        ArModePanel = 4,
        PlayModePanel = 5
    }

    [System.Serializable]
    public class PanelInfo
    {
       public int id;
       public PanelType key;
       public GameObject PanelObject;
    }

    public class MenuCanvasManager : MonoBehaviour
    {   

        [SerializeField]List<PanelInfo> _panelList;

        private void Start()
        {
            OpenPanel(0);//main menu
        }

        public void OpenPanel(int panelNo)
        {
            PanelType type = (PanelType)panelNo;

            foreach (var panel in _panelList)
            {
                if (type==panel.key)
                {
                    panel.PanelObject.SetActive(true);
                }
                else
                {
                    panel.PanelObject.SetActive(false);
                }
            }
        }

        public void LoadEditMode()
        {
            GameManager.instance.SwitchGameMode(GameMode.EditMode);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        public void LoadPlayMode()
        {
            GameManager.instance.SwitchGameMode(GameMode.PlayMode);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
       


    }
}
