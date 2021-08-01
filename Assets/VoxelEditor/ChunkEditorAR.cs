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
            Vector3 pos;
            pos.x = Mathf.Round(newBlockPos.x /scale);
            pos.y = Mathf.Round(newBlockPos.y / scale);
            pos.z = Mathf.Round(newBlockPos.z / scale);
            pos *= scale;
            pos += new Vector3(0.05f, 0.0f, 0.05f);
            placeHolderPos = pos;
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

            Debug.Log("newBlockPos" + newBlockPos);
            Debug.Log("hit point" + hit.point);
            Debug.Log("pos" + pos);


            int size = _chunkSO.Size;
            if (newBlockPos.x >= 0 && newBlockPos.y >= 0 && newBlockPos.z >= 0 &&
                newBlockPos.x < size && newBlockPos.y < size && newBlockPos.z < size)
            {

                _chunkSO.SetCell(pos, false);

                OnChunkUpdate?.Invoke(_chunkSO);
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

            Debug.Log("newBlockPos" + newBlockPos);
            Debug.Log("hit point" + hit.point);
            Debug.Log("pos" + pos);


            int size = _chunkSO.Size;
            if (newBlockPos.x >= 0 && newBlockPos.y >= 0 && newBlockPos.z >= 0 &&
                newBlockPos.x < size && newBlockPos.y < size && newBlockPos.z < size)
            {

                _chunkSO.SetCell(pos, true);

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
