using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkEditorAR : MonoBehaviour
{
    [SerializeField]
    Chunk _chunkSO;

    [SerializeField]
    GameObject placeHolderCube;

    [SerializeField]
     Camera _arCamera;

    Vector3 placeHolderPos=Vector3.zero;

    bool create;
    bool destroy;

    public float scale;
    //event 
    public static Action<Chunk> OnChunkUpdate;



    private void FixedUpdate()
    {
        UpdatePlaceHolderPosition();
        //create new block
        if (create)
        {
            CreateBlock();
            create = false;
        }
        //destrou block
        if (destroy)
        {
            DestroyBlock();
            destroy = false;
        }
    }
    private void Update()
    {
        placeHolderCube.transform.position = placeHolderPos;

    }



    private void UpdatePlaceHolderPosition()
    {
        RaycastHit hit;
        Ray ray = new Ray(_arCamera.transform.position,_arCamera.transform.forward);
        Debug.DrawRay(_arCamera.transform.position, ray.direction * 20, Color.red);

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point + (hit.normal * scale) / 2f;
            Vector3 origin = transform.position;
            newBlockPos = newBlockPos - origin;

            newBlockPos /= scale;
            Vector3 pos;
            pos.x = (float)Math.Round(newBlockPos.x, MidpointRounding.AwayFromZero);
            pos.y = (float)Math.Round(newBlockPos.y, MidpointRounding.AwayFromZero);
            pos.z = (float)Math.Round(newBlockPos.z, MidpointRounding.AwayFromZero);
            pos *= scale;
            placeHolderPos = pos+origin;
        }
    }

    private void DestroyBlock()
    {
        RaycastHit hit;
        Ray ray = new Ray(_arCamera.transform.position, _arCamera.transform.forward);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point - (hit.normal * scale) / 2f;
            Vector3 origin = transform.position;
            newBlockPos = newBlockPos - origin;
            transform.position = Vector3.zero;
            Vector3 pos;

            Debug.DrawRay(_arCamera.transform.position, ray.direction * 20, Color.red);

            pos.x = (float)Math.Round(newBlockPos.x / scale, MidpointRounding.AwayFromZero);
            pos.y = (float)Math.Round(newBlockPos.y / scale, MidpointRounding.AwayFromZero);
            pos.z = (float)Math.Round(newBlockPos.z / scale, MidpointRounding.AwayFromZero);

           /* Debug.Log("newBlockPos" + newBlockPos);
            Debug.Log("hit point" + hit.point);
            Debug.Log("pos" + pos);
*/

            int size = _chunkSO.Size;
            if (pos.x >= 0 && pos.y >= 0 && pos.z >= 0 &&
                pos.x < size && pos.y < size && pos.z < size)
            {

                _chunkSO.SetCell(pos, false);
                Debug.Log("Created");

                OnChunkUpdate?.Invoke(_chunkSO);
            }
            else
            {
                Debug.Log("");
            }
            transform.position = origin;
        }
    }

    private void CreateBlock()
    {
        RaycastHit hit;
        Ray ray = new Ray(_arCamera.transform.position, _arCamera.transform.forward);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point + (hit.normal * scale) / 2f;
            Vector3 origin = transform.position;
            newBlockPos = newBlockPos - origin;
            transform.position = Vector3.zero;
            Vector3 pos;


            pos.x = (float)Math.Round(newBlockPos.x / scale, MidpointRounding.AwayFromZero);
            pos.y = (float)Math.Round(newBlockPos.y / scale, MidpointRounding.AwayFromZero);
            pos.z = (float)Math.Round(newBlockPos.z / scale, MidpointRounding.AwayFromZero);

            /*Debug.Log("newBlockPos" + newBlockPos);
            Debug.Log("hit point" + hit.point);
            Debug.Log("pos" + pos);
*/

            int size = _chunkSO.Size;
            if (pos.x >= 0 && pos.y >= 0 && pos.z >= 0 &&
                pos.x < size && pos.y < size && pos.z < size)
            {

                _chunkSO.SetCell(pos, true);
                Debug.Log("Destroyed");

                OnChunkUpdate?.Invoke(_chunkSO);
            }
            transform.position = origin;
        }
    }

    public void OnAddButton()
    {
        create = true;
    }

    public void OnDestroyButton()
    {
        destroy = true;
    }
}
