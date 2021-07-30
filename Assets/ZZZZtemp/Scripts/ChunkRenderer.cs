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

    [SerializeField]
    Gradient gradient;

    List<Vector3> _vertices;
    List<int> _triangles;
    List<Color> _vertexColor;

    

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
       // GenerateBasicChunkMesh(_chunkSO);
        GenerateTerrainChunkMesh(_chunkSO);
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

    private void GenerateBasicChunkMesh(Chunk chunkData)
    {
        _vertices = new List<Vector3>();
        _triangles = new List<int>();


        for (int x = 0; x < chunkData.Size; x++)
        {
                for (int z = 0; z < chunkData.Size; z++)
                {
                chunkData.SetCell(x, 0, z, true);
                }
        }

        GenerateChunkMesh(chunkData);
    }

    private void GenerateTerrainChunkMesh(Chunk chunkData)
    {
        _vertices = new List<Vector3>();
        _triangles = new List<int>();

        int randomVal = UnityEngine.Random.Range(1, 10);
        for (int x = 0; x < chunkData.Size; x++)
        {
            for (int z = 0; z < chunkData.Size; z++)
            {

                int y = CalculateHeight(x, z, randomVal);

                for (int i = 1; i < y; i++)
                {
                    chunkData.SetCell(x, i, z, true);
                }
                chunkData.SetCell(x, 0, z, true);
            }
        }

        GenerateChunkMesh(chunkData);
    }

    private int CalculateHeight(int x,int y,int seed)
    {
        float xCoord = (float)x / 10;
        float yCoord = (float)y / 10;
        float sample = Mathf.PerlinNoise(xCoord+seed, yCoord+ seed);
        sample = Mathf.Clamp(sample, 0f, 1f);
        sample *= 10;
        return (int)sample;
    }

}
