using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer),typeof(MeshCollider))]
public class ChunkRenderer : MonoBehaviour
{
    [SerializeField]
    Chunk _chunkSO;
    [SerializeField]
    MeshFilter _meshFilter;
    [SerializeField]
    MeshCollider _meshCollider;
    [Space]
    [SerializeField]
    float _scale = 1f;



    List<Vector3> _vertices;
    List<int> _triangles;

    #region MonoBehaviourCallBacks

    private void OnEnable()
    {
        ChunkEditor.OnChunkUpdate += GenerateChunkMesh;
    }

    private void OnDisable()
    {
        ChunkEditor.OnChunkUpdate -= GenerateChunkMesh;

    }

    private void Start()
    {
        GenerateChunkMesh(_chunkSO);
        
    }

    #endregion
    

    private void GenerateChunkMesh(Chunk chunkData)
    {
        _vertices = new List<Vector3>();
        _triangles = new List<int>();
        for (int x = 0; x < chunkData.Size; x++)
        {
            for (int y = 0; y < chunkData.Size; y++)
            {
                for (int z = 0; z < chunkData.Size; z++)
                {
                    if (chunkData.GetCell(x, y, z) == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Vector3 cubePos = new Vector3((float)x * _scale, (float)y * _scale, (float)z * _scale);
                        MakeCube(_scale, cubePos, x, y, z, chunkData);
                    }
                }
            }
        }
        UpdateMesh();
    }

    private void MakeCube(float scale, Vector3 cubePos, int x, int y, int z, Chunk chunkData)
    {
        for (int i = 0; i < 6; i++)
        {
            if (chunkData.GetNeighbour(x, y, z, (Direction)i) == 0)
            {
                MakeFace((Direction)i, scale, cubePos);
            }

        }
    }

    private void MakeFace(Direction dir, float faceScale, Vector3 facePos)
    {

        _vertices.AddRange(CubeMeshInfo.faceVertices((int)dir, faceScale, facePos));
        int vCount = _vertices.Count - 4;

        _triangles.Add(vCount + 0);
        _triangles.Add(vCount + 1);
        _triangles.Add(vCount + 2);

        _triangles.Add(vCount + 0);
        _triangles.Add(vCount + 2);
        _triangles.Add(vCount + 3);
    }

    private void UpdateMesh()
    {
        Mesh mesh= _meshFilter.mesh;
        mesh.Clear();
        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();
        mesh.RecalculateNormals();

        _meshCollider.sharedMesh = null;
        _meshCollider.sharedMesh = mesh;
        Debug.Log("mesh updated");

    }




}
