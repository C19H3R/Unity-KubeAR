using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace KubeWorldAR
{
    public enum EditState
    {
        PlacingWorld,
        WorldPlaced
    }

    public class ArModeState
    {

        public static Action<EditState> OnEditStateChanged;


        public static EditState CurrentState=EditState.PlacingWorld;

        public static void changeEditState(EditState newState)
        {
            
            CurrentState= newState;

            OnEditStateChanged?.Invoke(newState);
        }
    }

}
