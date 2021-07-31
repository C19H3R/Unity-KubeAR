using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkEditor : MonoBehaviour
{
    [SerializeField]
    Chunk _chunkSO;

    [SerializeField]
    GameObject placeHolderCube;

    Vector3 placeHolderPos;

    //event 
    public static Action<Chunk> OnChunkUpdate;

    private void FixedUpdate()
    {
        UpdatePlaceHolderPosition();
        
        //create new block
        if (Input.GetMouseButtonDown(0))
        {
            CreateBlock();
        }
        //destrou block
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyBlock();

        }
    }

    private void Update()
    {
        placeHolderCube.transform.position = placeHolderPos;
    }

    private void UpdatePlaceHolderPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 20, Color.red);

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point + hit.normal / 2f;
            Vector3 pos;
            pos.x = Mathf.Round(newBlockPos.x);
            pos.y = Mathf.Round(newBlockPos.y);
            pos.z = Mathf.Round(newBlockPos.z);

            placeHolderPos = pos;
        }
    }

    private void DestroyBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point - hit.normal / 2f;
            Vector3 origin = transform.position;
            newBlockPos = newBlockPos - origin;
            transform.position = Vector3.zero;
            Vector3 pos;

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 20, Color.red);

            pos.x = (float)Math.Round(newBlockPos.x, MidpointRounding.AwayFromZero);
            pos.y = (float)Math.Round(newBlockPos.y, MidpointRounding.AwayFromZero);
            pos.z = (float)Math.Round(newBlockPos.z, MidpointRounding.AwayFromZero);

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point + hit.normal / 2f;
            Vector3 origin = transform.position;
            newBlockPos = newBlockPos - origin;
            transform.position = Vector3.zero;
            Vector3 pos;

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 20, Color.red);

            pos.x = (float)Math.Round(newBlockPos.x, MidpointRounding.AwayFromZero);
            pos.y = (float)Math.Round(newBlockPos.y, MidpointRounding.AwayFromZero);
            pos.z = (float)Math.Round(newBlockPos.z, MidpointRounding.AwayFromZero);

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
}

