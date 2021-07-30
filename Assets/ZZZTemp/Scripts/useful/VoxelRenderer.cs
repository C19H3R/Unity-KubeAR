using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRenderer : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    public float scale = 1f;

    float adjScale;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f;
    }
    private void Start()
    {
        GenerateVoxelMesh(new VoxelData());
        UpdateMesh();
    }

    private void GenerateVoxelMesh(VoxelData voxelData)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        for (int x = 0; x < voxelData.Size; x++)
        {
            for (int y = 0; y < voxelData.Size; y++)
            {
                for (int z = 0; z < voxelData.Size; z++)
                {
                    if (voxelData.GetCell(x, y, z) == 0)
                    {
                        continue;
                    }
                    else
                    {
                        MakeCube(adjScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale),x,y,z,voxelData);
                    }
                }
            }
        }
    }

    void MakeCube(float cubeScale, Vector3 cubePos,int x,int y ,int z,VoxelData data)
    {
       
        for (int i = 0; i < 6; i++)
        {
            if (data.GetNeighbour(x, y, z, (Direction)i) == 0)
            {
                MakeFace((Direction)i, cubeScale, cubePos);
            }

        }
    }
    void MakeFace(Direction dir, float faceScale, Vector3 facePos)
    {
        vertices.AddRange(CubeMeshData.faceVertices((int)dir, faceScale, facePos));
        int vCount = vertices.Count - 4;

        triangles.Add(vCount + 0);
        triangles.Add(vCount + 1);
        triangles.Add(vCount + 2);

        triangles.Add(vCount + 0);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 3);
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

}

