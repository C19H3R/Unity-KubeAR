using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KubeWorldAR {

    [System.Serializable]
    public enum PanelType
    {
        MainMenu = 0,
        Settings = 1,
        Controls = 2,
        HowToPlay = 3,
    }


    [System.Serializable]
    public class PanelInfo
    {
       public int id;
       public PanelType key;
       public GameObject PanelObject;
    }

    public class MenuCanvasManager : MonoBehaviour
    {   [SerializeField]
        PanelType type;

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
        


       


    }
}
