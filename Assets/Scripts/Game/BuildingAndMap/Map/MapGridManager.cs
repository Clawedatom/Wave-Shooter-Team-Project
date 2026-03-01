using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MapGridManager : MonoBehaviour
{
    #region Class References
    MapCreationManager mapCreationManager;
    #endregion

    #region Private Fields
    [SerializeField] private bool showGrid;

    
    [SerializeField] private GridSquare[,] gridArray; // 2d array of grid points


    [Header("Grid Settings")]

    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;

    [SerializeField] private float cellSize = 3f;

    private Vector3 gridOrigin;
    private float halfWidth;
    private float halfHeight;




    #endregion

    #region Properties
    public GridSquare[,] GridArray
    {
        get { return gridArray; }
        set { gridArray = value; }
    }
    #endregion

    #region Start Up
    public void OnAwake()
    {
        mapCreationManager = GetComponent<MapCreationManager>();


        gridArray = new GridSquare[gridWidth, gridHeight];

        mapCreationManager.OnAwake();
    }

    public void OnStart()
    {
        gridOrigin = transform.position;
        halfWidth = (gridWidth * cellSize) * 0.5f;
        halfHeight = (gridHeight * cellSize) * 0.5f;

        InitialiseGrid();


        mapCreationManager.OnStart(gridArray);
    }
    #endregion

    #region Update Functions
    public void OnUpdate()
    {
        //VisualiseGrid();
    }
    #endregion

    #region Class Functions
  
    public Vector2 WorldPositionToGridCenter(Vector2 worldPos)
    {
        Vector2Int index = WorldPositiontoGridIndex(worldPos);
        return GridIndexToGridCenter(index.x, index.y);
    }

    public Vector2 GridIndexToGridCenter(int x, int y)
    {
       
        if (x < 0 || y < 0 || x >= gridWidth || y >= gridHeight) return Vector2.zero;
        return gridArray[x, y].worldPosition;
    }

    public Vector2Int WorldPositiontoGridIndex(Vector3 worldPos)
    {
        Vector2Int gridIndex = Vector2Int.zero;
        gridIndex.x = Mathf.FloorToInt((worldPos.x - gridOrigin.x + halfWidth) / cellSize);
        gridIndex.y = Mathf.FloorToInt((worldPos.y - gridOrigin.y + halfHeight) / cellSize);
        return gridIndex;
    }

    

    public Vector2 GridIndexToWorldPosition(Vector2Int gridIndex)
    {
        float x = (gridIndex.x * cellSize) + (gridOrigin.x - halfWidth) + (cellSize * 0.5f);
        float y = (gridIndex.y * cellSize) + (gridOrigin.y - halfHeight) + (cellSize * 0.5f);
        return new Vector2(x, y);
    }




    private void InitialiseGrid() //set up grid, doesnt really do much atm
    {
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                Vector2Int gridPos = new Vector2Int(i, j);
                Vector2 worldPos = GridIndexToWorldPosition(gridPos);
                gridArray[i, j] = new GridSquare(gridPos.x, gridPos.y, worldPos.x, worldPos.y);
            }
        }
    }

    public void VisualiseGrid()
    {
        showGrid = !showGrid;
       
    }

    private void OnDrawGizmos()
    {
        if (!showGrid) return;

        gridOrigin = transform.position;
        halfWidth = (gridWidth * cellSize) * 0.5f;
        halfHeight = (gridHeight * cellSize) * 0.5f;

        Gizmos.color = Color.black;

        // ddraw vertical lines
        for (int x = 0; x <= gridWidth; x++)
        {
            float xPos = (x * cellSize) - halfWidth; // centering horizontally
            Vector3 startPoint = new Vector3(xPos + gridOrigin.x, gridOrigin.y - halfHeight, gridOrigin.z);
            Vector3 endPoint = new Vector3(xPos + gridOrigin.x, gridOrigin.y + halfHeight, gridOrigin.z);
            Gizmos.DrawLine(startPoint, endPoint);
        }

        //draw horizontal lines
        for (int y = 0; y <= gridHeight; y++)
        {
            float yPos = (y * cellSize) - halfHeight; // centering vertically
            Vector3 startPoint = new Vector3(gridOrigin.x - halfWidth, yPos + gridOrigin.y, gridOrigin.z);
            Vector3 endPoint = new Vector3(gridOrigin.x + halfWidth, yPos + gridOrigin.y, gridOrigin.z);
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }


    public float GetCellSize()
    {
        return cellSize;
    }

    public bool CheckGridTaken(Vector2Int gridPos)
    {
        //print("IS Obstacle: " + gridArray[gridPos.x, gridPos.y].isObstacle);
        //print("IS turretL " + gridArray[gridPos.x, gridPos.y].isTurret);
        return gridArray[gridPos.x, gridPos.y].isObstacle || gridArray[gridPos.x, gridPos.y].isTurret;
    }

    public bool CheckOutOfBounds(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x > gridArray.GetLength(0) || gridPos.y < 0 || gridPos.y > gridArray.GetLength(1))
        {
            return true;
        }
        return false;
    }

    public void SetGridTurret(Vector2Int gridpos)
    {
        gridArray[gridpos.x, gridpos.y].isTurret = true;
    }
    #endregion
}


[System.Serializable]
public class GridSquare
{
    public GridSquare(int x, int y, float worldX, float worldY)
    {
        gridPosition.x = x;
        gridPosition.y = y;
        worldPosition.x = worldX;
        worldPosition.y = worldY;
    }


    public bool isObstacle;
    public bool isTurret;

    public Vector2 gridPosition;
    public Vector2 worldPosition;
    public bool canBuild; // bool to see if you can place buildings on the grid point


}
