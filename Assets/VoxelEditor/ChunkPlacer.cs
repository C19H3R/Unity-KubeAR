using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


namespace KubeWorldAR
{



    [RequireComponent(typeof(ARRaycastManager))]
    public class ChunkPlacer : MonoBehaviour
    {


        [SerializeField]
        GameObject _chunkObject;

        [SerializeField]
        GameObject _arrowrrowObject;


        [SerializeField]
        ARRaycastManager _aRRaycastManager;
        


        static List<ARRaycastHit> hits = new List<ARRaycastHit>();

        Vector3 screenCenter;
        bool isPlaced = false;

        bool placedOnce = false;

        private void OnEnable()
        {
            //GameManager.OnGameModeChange += onGameModeChange;

            Invoke("onGameModeChangeEditMode", 0.5f);
        }
        private void OnDisable()
        {
            //GameManager.OnGameModeChange -= onGameModeChange;
        }

        Vector3 initialPosChunk;

        private void Awake()
        {
            initialPosChunk = _chunkObject.transform.position;
            screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        }


        private void Update()
        {
            if (!placedOnce && _aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (isPlaced)
                {
                    _chunkObject.transform.position = hitPose.position;
                    placedOnce = true;
                    _arrowrrowObject.SetActive(false);
                    ArModeState.changeEditState(EditState.WorldPlaced);
                }
                else
                {
                    _arrowrrowObject.transform.position = hitPose.position;
                }
            }
        }
        
        public void PlaceObjectHere()
        {
            isPlaced = true;
        }


        public void onGameModeChangeEditMode()
        {
            Debug.Log("Hola");
            GameMode newMode=GameMode.EditMode;
            switch (newMode)
            {
                case GameMode.EditMode:
                    _chunkObject.transform.position = initialPosChunk;
                    placedOnce = false;
                    isPlaced = false;
                    _arrowrrowObject.SetActive(true);
                    ArModeState.changeEditState(EditState.PlacingWorld);
                    break;
                case GameMode.PlayMode:
                    break;
                default:
                    break;
            }
        }

    }

}