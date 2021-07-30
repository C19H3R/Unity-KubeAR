using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData : MonoBehaviour
{
    int[,,] data = new int[,,]{
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

    public int Size
    {
        get { return data.GetLength(0); }
    }

    public int GetCell(int x, int y, int z)
    {
        return data[x, y, z];
    }
    public int GetNeighbour(int x, int y, int z, Direction dir)
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

    struct DataCoordinate
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