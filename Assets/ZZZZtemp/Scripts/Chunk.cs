using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChunkData", menuName = "ScriptableObjects/ChunkSO", order = 1)]
public class Chunk : ScriptableObject
{
    [SerializeField]
    int size = 3;

    public int Size
    {
        get
        {
            return size;
        }
    }
    [SerializeField]
    int[,,] chunkData= new int[,,]{
        {
        { 1,1,1 },
        { 1,1,1 },
        { 1,1,1 },
        },
        {
        { 0,0,0 },
        { 0,1,0 },
        { 0,0,0 },
        },
        {
        { 0,1,0 },
        { 1,1,1 },
        { 0,1,0 },
        },
    };

    public int GetCell(int x, int y, int z)
    {
        return chunkData[x, y, z];
    }
    public void SetCell(int x, int y, int z,bool visible)
    {
        if (visible)
        {
            chunkData[x, y, z] = 1;
        }
        else
        {
            chunkData[x, y, z] = 0;
        }
    }

    public void SetCell(Vector3 cellPos,bool visible)
    {
        SetCell((int)cellPos.x, (int)cellPos.y, (int)cellPos.z, visible);
    }
    public int GetNeighbour(int x,int y,int z,Direction dir)
    {
        DataCoordinate offsetToCheck = offsets[(int)dir];
        DataCoordinate neighbourCoordinate = new DataCoordinate(x + offsetToCheck.x, y + offsetToCheck.y, z + offsetToCheck.z);

        if (neighbourCoordinate.x < 0 || neighbourCoordinate.x >= Size || neighbourCoordinate.y < 0 || neighbourCoordinate.y >= Size || neighbourCoordinate.z < 0 || neighbourCoordinate.z >= Size)
        {
            return 0;
        }
        else
        {
            return GetCell(neighbourCoordinate.x, neighbourCoordinate.y, neighbourCoordinate.z);
        }
    }
    DataCoordinate[] offsets =
    {
        new DataCoordinate(0, 0, 1),
        new DataCoordinate(1, 0, 0),
        new DataCoordinate(0, 0, -1),
        new DataCoordinate(-1, 0, 0),
        new DataCoordinate(0, 1, 0),
        new DataCoordinate(0, -1, 0),

    };

}

public enum Direction
{
    North,
    East,
    South,
    West,
    Up,
    Down
}

public struct DataCoordinate
{
    public int x;
    public int y;
    public int z;

    public DataCoordinate(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}