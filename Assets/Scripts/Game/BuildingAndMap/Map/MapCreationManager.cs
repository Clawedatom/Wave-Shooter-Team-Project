using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MapCreationManager : MonoBehaviour
{
    MapGridManager mapGridManager;

    [SerializeField] private List<GameObject> obstacleGridPoints = new List<GameObject>();

    [SerializeField] private int fillPointNeighboursMin = 3;

    [SerializeField] private int gapFixAttempts = 2;

    [SerializeField] private int numberOfSmallClusters = 3;

    [SerializeField] private int smallClusterRadius = 3;

    [SerializeField] private float clusterDensity = 0.5f; // noise


    [SerializeField] private int numberOfLargeClusters = 1;
    [SerializeField] private int largeClusterRadius = 6;

    [SerializeField] private int maxClusterAttempts = 2; //prevent stack overflow

    [SerializeField] private int centerCheckOffset;
    [SerializeField] private GameObject obstaclePrefab;

    public GridSquare[,] gameGrid
    {
        get { return mapGridManager.GridArray; }
        set { mapGridManager.GridArray = value;}
    }


    public void OnAwake()
    {
        mapGridManager = GetComponent<MapGridManager>();
    }

    

    
    public void OnStart(GridSquare[,] gridArray) // block the border off
    {
        centerCheckOffset = largeClusterRadius + 3;

        gameGrid = gridArray;
        GenerateMap();

        for (int i = 0; i < gapFixAttempts; i++)
        {
            FillClusterGaps();

        }
    }

    private void GenerateMap()
    {
        CreateBorder();


        for (int i = 0; i < numberOfSmallClusters; i++)
        {
            //create 5 clusters

            CreateCluster(smallClusterRadius, 0);
        }

        for (int j = 0; j < numberOfLargeClusters; j++)
        {
            CreateCluster(largeClusterRadius, 0);
        }
    }

    private void CreateBorder()
    {
        
        for (int x = 0; x < gameGrid.GetLength(0); x++) // loops through top asnd bot
        {
           
            GameObject ob1 = CreateObstacle(new Vector2Int(x, 0));
            
            GameObject ob2 = CreateObstacle(new Vector2Int(x, gameGrid.GetLength(0) - 1));
            AddObstacleToMapArray(ob1);
            AddObstacleToMapArray(ob2);
        }

        
        for (int y = 1; y < gameGrid.GetLength(1) - 1; y++) // loops tthough left and right
        {
            
            GameObject ob1 = CreateObstacle(new Vector2Int(0, y));

            GameObject ob2 = CreateObstacle(new Vector2Int(gameGrid.GetLength(1) - 1, y));

            AddObstacleToMapArray(ob1);
            AddObstacleToMapArray(ob2);
        }
    }

    private void FillClusterGaps()
    {
        
        for (int x = 0; x < gameGrid.GetLength(0); x++)
        {
            for (int y = 0; y < gameGrid.GetLength(1) - 1; y++)
            {
                //check if not obstacle
                if (!gameGrid[x, y].isObstacle)
                {
                    //check 
                    int neighbours = CheckNeighbours(new Vector2Int(x, y));
                    if (neighbours >= fillPointNeighboursMin)
                    {
                        GameObject obGo = CreateObstacle(new Vector2Int(x, y));
                        AddObstacleToMapArray(obGo);
                        //Debug.Log("Added obstacle at " + x + " " + y);
                    }
                }
            }
        }
    }

    private int CheckNeighbours(Vector2Int gridPos)
    {
        int neighbourCount = 0;

        List<Vector2Int> neighbours = new List<Vector2Int>();


        neighbours = GetNeighbours(gridPos);
        

        foreach(Vector2Int neighbour in neighbours)
        {
            if (IsWithinBounds(neighbour) && IsObstacle(neighbour))
            {
                neighbourCount++;
            }
        }
        return neighbourCount;
    }

    private List<Vector2Int> GetNeighbours(Vector2Int gridPos)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        Vector2Int northPos = new Vector2Int(gridPos.x, gridPos.y + 1);
        Vector2Int southPos = new Vector2Int(gridPos.x, gridPos.y - 1);
        Vector2Int eastPos = new Vector2Int(gridPos.x + 1, gridPos.y);
        Vector2Int westPos = new Vector2Int(gridPos.x - 1, gridPos.y);
        neighbours.Add(northPos);
        neighbours.Add(southPos);
        neighbours.Add(eastPos);
        neighbours.Add(westPos);

        return neighbours;

    }

   
    private bool IsWithinBounds(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < gameGrid.GetLength(0) && gridPos.y >= 0 && gridPos.y < gameGrid.GetLength(1);
    }

    private bool IsObstacle(Vector2Int gridPos)
    {
        return gameGrid[gridPos.x, gridPos.y].isObstacle;
    }


    private void CreateCluster(int clusterSize, int clusterAttempts)
    {
        if (clusterAttempts == maxClusterAttempts) // stops trying after a few attempts to prevent memory issues ( i think issues happen lol, happned once)
        {
            Debug.Log("Failed to create a cluster after trying " + clusterAttempts + " times");
            return;
        }
        List<Vector2Int> currentCluster = new List<Vector2Int>();

        Vector2Int gridPoint = GetRandomGridPoint(clusterSize);
        bool canPlace = true; // check if obstacle point is taken

        Vector2Int clusterCenter = new Vector2Int(clusterSize / 2, clusterSize / 2);

        

        Vector2Int gridCenter = new Vector2Int(gameGrid.GetLength(0) / 2, gameGrid.GetLength(1) / 2);

        float distanceFromCenter = Vector2Int.Distance(gridPoint, gridCenter);

        
        if (distanceFromCenter <= centerCheckOffset)
        {
            Debug.Log("Attempted to make cluster in center of map, retrying");
            canPlace = false;
        }
        if (canPlace)// if passed the test of being away frokm center
        {
            for (int x = 0; x < clusterSize; x++)
            {
                for (int y = 0; y < clusterSize; y++) //loops through all grid points
                {

                    float clusterNoiseVAl = Mathf.PerlinNoise((gridPoint.x + x) * clusterDensity, (gridPoint.y + y) * clusterDensity); // gets a noise value based on the value of neighbouring points so that it looks more natural 
                    //stops checkered patterns and similar stuff from comining up which would be the case if noise wasnt used
                    float distance = Vector2Int.Distance(new Vector2Int(x, y), clusterCenter); // by checking dist between center and target grid point and limiting it to half the size of the cluster. can make it more circular
                                                                                               // because it essentially makes a radius around it

                    if (clusterNoiseVAl < 0.01f || Random.value < 0.3f || distance > clusterSize / 2) continue; // checks numbers again threshold and another random value for a final check to reduce density, can remove to have different results.

                    int gridX = gridPoint.x + x;
                    int gridY = gridPoint.y + y;

                    if (gameGrid[gridX, gridY].isObstacle) // checkls if already taken
                    {
                        canPlace = false;
                        break;
                    }

                    Vector2Int gridPos = new Vector2Int(gridX, gridY);
                    currentCluster.Add(gridPos); // current checked point is clear
                }

                if (!canPlace) break;
            }

            if (!canPlace) // if obstacle point is taken then kill the cluster and try again
            {
                currentCluster.Clear(); // redundent but for clarity, essentially stops trying to make cluster

                // Retry cluster creation
                Debug.Log("Remaking cluster due to overlap");
                clusterAttempts++;
                CreateCluster(clusterSize, clusterAttempts);
                return;
            }

            //if successs add to list of obstacles

            foreach (Vector2Int clusterV2 in currentCluster) // if all points checked are kewl then make then obstacles
            {
                GameObject clusterGO = CreateObstacle(clusterV2);
                AddObstacleToMapArray(clusterGO);
            }
        }
        
        
    }


    private Vector2Int GetRandomGridPoint(int clusterSize)
    {
        Vector2Int gridPoint = new Vector2Int(Mathf.RoundToInt(Random.Range(0, gameGrid.GetLength(0) - (clusterSize + 3))),Mathf.RoundToInt(Random.Range(0, gameGrid.GetLength(1) - (clusterSize + 3))));   //gets random grid point
        return gridPoint;
    }
    
    private GameObject CreateObstacle(Vector2Int gridPos)
    {
        Vector2 gridCenter = mapGridManager.GridIndexToGridCenter(gridPos.x, gridPos.y);

        GameObject obstacleGO = Instantiate(obstaclePrefab, gridCenter, Quaternion.identity, this.transform);

        

        return obstacleGO;
    }


    private void AddObstacleToMapArray(GameObject clusterGO)
    {
        obstacleGridPoints.Add(clusterGO);
        
        Vector2Int gridPos = mapGridManager.WorldPositiontoGridIndex(clusterGO.transform.position);

        gameGrid[gridPos.x, gridPos.y].isObstacle = true;
    }
}
