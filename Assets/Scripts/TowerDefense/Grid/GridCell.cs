using Unity.Mathematics;
using Vector3 = UnityEngine.Vector3;

namespace TowerDefense.Grid
{
    /// <summary>
    /// Represents on grid cell
    /// </summary>
    public struct GridCell
    {
        //cell coordinate
        public int2 Coord;
        //Pathfinding score
        public float3 PathScore;
        //Neighbour cell
        public int2 Parent;
        //World position
        public Vector3 WorldPosition;
        //reference to what tower was placed
        public int TowerId;

        public bool IsOccupied
        {
            get { return TowerId > 0;}
        }

        public void SetTowerId(int id)
        {
            TowerId = id;
        }

    }
}