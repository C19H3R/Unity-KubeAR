using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkEditor : MonoBehaviour
{
    [SerializeField]
    Chunk _chunkSO;

    //event 
    public static Action<Chunk> OnChunkUpdate;

    private void Update()
    {
        //create new block
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit, 1000f))
            {
                Vector3 newBlockPos = hit.point + hit.normal / 2f;

                Vector3 pos;

                Debug.DrawRay(Camera.main.transform.position, ray.direction * 20,Color.red);

                pos.x = (float)Math.Round(newBlockPos.x, MidpointRounding.AwayFromZero);
                pos.y = (float)Math.Round(newBlockPos.y, MidpointRounding.AwayFromZero);
                pos.z = (float)Math.Round(newBlockPos.z, MidpointRounding.AwayFromZero);

                Debug.Log("newBlockPos"+ newBlockPos);
                Debug.Log("hit point" + hit.point);
                Debug.Log("pos" + pos);


                int size = _chunkSO.Size;
                
                    _chunkSO.SetCell(pos,true);

                    OnChunkUpdate?.Invoke(_chunkSO);

            }
        }
        //destrou block
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
}
