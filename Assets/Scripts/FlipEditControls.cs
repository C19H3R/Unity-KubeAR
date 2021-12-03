using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KubeWorldAR
    
{

    public class FlipEditControls : MonoBehaviour
    {
        [SerializeField]
        GameObject PlacingMode;
        [SerializeField]
        GameObject EditingMode;

        private void OnEnable()
        {
            ArModeState.OnEditStateChanged += onEditModeChange;
        }
        private void OnDisable()
        {
            ArModeState.OnEditStateChanged -= onEditModeChange;
        }

        private void Start()
        {
            ArModeState.changeEditState(EditState.PlacingWorld);
        }

        public void onEditModeChange(EditState newState)
        {
            switch (newState)
            {
                case EditState.PlacingWorld:
                    PlacingMode.SetActive(true);
                    EditingMode.SetActive(false);
                    break;
                case EditState.WorldPlaced:
                    PlacingMode.SetActive(false);
                    EditingMode.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
