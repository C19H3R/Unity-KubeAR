using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ChunkPlacer : MonoBehaviour
{
    [SerializeField]
    GameObject _chunkObject;

    


    [SerializeField]
    ARRaycastManager _aRRaycastManager;
    Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {

    }

   

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }
        bool isOverUi=IsPointerOverUIObject();
        if (!isOverUi&&_aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            _chunkObject.transform.position = hitPose.position;
           
            

        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

}
