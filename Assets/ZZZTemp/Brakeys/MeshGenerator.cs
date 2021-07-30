using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public int size;

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(size + 1) * (size + 1)];

        for (int i = 0, z = 0; z <= size; z++)
        {
            for (int x = 0; x <= size; x++)
            {

                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }


        triangles = new int[size*size*6];

        int vert = 0, tris = 0;

        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size + 1;
                triangles[tris + 5] = vert + size + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
       


    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
