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

    Vector3 hitPosition;

    bool create;
    bool destroy;

    //event 
    public float scale;
    public static Action<Chunk> OnChunkUpdate;

    private void FixedUpdate()
    {
        UpdatePlaceHolderPosition();
        
        //create new block
        if (create)
        {
            CreateBlock();
            create=false;
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

        if (Input.GetMouseButtonDown(0))
            create = true;

        if (Input.GetKeyDown(KeyCode.Space))
            destroy = true;
    }

    private void UpdatePlaceHolderPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 1000f))
        {

            Debug.DrawRay(Camera.main.transform.position, ray.direction*20, Color.yellow);
            Vector3 newBlockPos = hit.point + (hit.normal*scale) / 2f;

            Vector3 origin=transform.position;
            newBlockPos = newBlockPos - origin;

            newBlockPos /= scale;
            Vector3 pos;
            pos.x = (float)Math.Round(newBlockPos.x ,MidpointRounding.AwayFromZero);
            pos.y = (float)Math.Round(newBlockPos.y , MidpointRounding.AwayFromZero);
            pos.z = (float)Math.Round(newBlockPos.z , MidpointRounding.AwayFromZero);

            pos *= scale;

            Debug.Log(hit.normal);
            hitPosition = hit.point;
            Debug.DrawRay(hitPosition,hit.normal * scale / 2f, Color.red);


            placeHolderPos = pos;
        }
    }

    private void DestroyBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point - hit.normal * scale / 2f;
            Vector3 origin = transform.position;
            newBlockPos = newBlockPos - origin;
            transform.position = Vector3.zero;
            Vector3 pos;

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 20, Color.red);

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 newBlockPos = hit.point + hit.normal * scale / 2f;
            Vector3 origin = transform.position;
            newBlockPos = newBlockPos - origin;
            transform.position = Vector3.zero;
            Vector3 pos;

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 20, Color.red);

            pos.x = (float)Math.Round(newBlockPos.x/scale, MidpointRounding.AwayFromZero);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hitPosition, 0.005f);
        Gizmos.color = Color.yellow;
    }
}

