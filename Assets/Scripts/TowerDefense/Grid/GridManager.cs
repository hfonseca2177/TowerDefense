using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TowerDefense.Grid
{
    /// <summary>
    /// Controls the grid placement positions
    /// </summary>
    public class GridManager : MonoBehaviour
    {
        [Header("Controls the grid placement positions")]
        [Tooltip("Factor that defines the snapping scale relative to the grid position")]
        [SerializeField] private float _snapScale = 1.8f;
        [Tooltip("width in meters for the grid")]
        [SerializeField] private int _gridWidth = 43;
        [Tooltip("height in meters for the grid")]
        [SerializeField] private int _gridHeight = 20;
        [SerializeField] private Transform _startingPosition;
        
        public GridCell[,] _grid;
        private Hashtable _gridAllocations;
        private int _gridRows = 12; // z changes
        private int _gridCols = 24; // x changes
        private float _yOffset = 0.5f;

        private void Start()
        {
            InstantiateGrid();
        }

        public Vector3 SnapToGrid(Vector3 pointer)
        {
            //take the grid position to as starting reference
            Vector3 gridPosition = transform.position;
            pointer -= gridPosition;
            float xPos = SnapCoord(pointer.x); 
            float yPos = _yOffset;
            float zPos = SnapCoord(pointer.z);
            Vector3 snapResult = new Vector3(xPos, yPos, zPos);
            snapResult += gridPosition;
            return snapResult;
        }

        private float SnapCoord(float coord)
        {
            int adjustedCoord = Mathf.RoundToInt(coord / _snapScale);
            return adjustedCoord * _snapScale;
        }

        private void InstantiateGrid()
        {
            int maxAllocations = _gridRows * _gridCols;
            _gridAllocations = new Hashtable(maxAllocations);
            Vector3 baseCoord = _startingPosition.position;
            _grid = new GridCell[_gridRows, _gridCols];
            float x = baseCoord.x;
            float z = baseCoord.z;
            for (int i = 0; i < _gridRows; i++)
            {
                for (int j = 0; j < _gridCols; j++)
                {   
                    GridCell gridCell = new GridCell();
                    Vector3 point = SnapToGrid(new Vector3(x, _yOffset, z));
                    gridCell.Coord = new int2(i, j);
                    gridCell.TowerId = -1;
                    gridCell.WorldPosition = point;
                    _grid[i, j] = gridCell;
                    x += _snapScale;    
                }
                x = baseCoord.x;
                z += _snapScale;
            }
        }

        public void AllocateGrid(Vector3 position)
        {
            if (!IsAlreadyAllocated(position))
            {
                _gridAllocations.Add(position, true);    
            }
        }

        public bool IsAlreadyAllocated(Vector3 position)
        {
            return _gridAllocations.ContainsKey(position);
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 baseCoord = _startingPosition.position;
            float baseXPos = baseCoord.x;
            float baseZPos = baseCoord.z;
            float rowsLength = baseXPos + _gridWidth;  
            float colsLength = baseZPos + _gridHeight;
            Gizmos.color = Color.yellow;
            for (float x = baseXPos; x < rowsLength; x += _snapScale)
            {
                for (float z = baseZPos; z < colsLength; z += _snapScale)
                {
                    Vector3 point = SnapToGrid(new Vector3(x, _yOffset, z));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }
}
