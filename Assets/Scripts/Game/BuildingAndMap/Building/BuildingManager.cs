using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    #region Class References
    private static BuildingManager _instance;
    
    PlayerManager playerManager;
    MapGridManager mapGridManager;
    #endregion

    #region Private Fields
    

    [Header("Building Fields")]
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private List<TurretManager> turrets;




    [Header("Grid Point fields")]
    [SerializeField] private Vector2 currentHoveredGridCenter;

    [SerializeField] private GameObject testBuilding;
    [SerializeField] private GameObject mousePointGO;
    [SerializeField] private GameObject buildModeHoverGO;
    [SerializeField] private GameObject buildModeHoverPrefab;
    
    #endregion

    #region Properties
    public static BuildingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<BuildingManager>();
                if (_instance == null)
                {
                    Debug.LogError("BuildingManager has not been assigned");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Start Up
    public void OnAwake() //add when combining
    {
        playerManager = PlayerManager.Instance;


        mapGridManager = GetComponent<MapGridManager>();

        mapGridManager.OnAwake();
    }

    
    public void OnStart()
    {
        mapGridManager.OnStart();
    }
    #endregion

    #region Update Functions
    public void OnUpdate(bool buildMode)
    {
        mapGridManager.OnUpdate();

        if (!buildMode && buildModeHoverGO != null)
        {
            Destroy(buildModeHoverGO);
            buildModeHoverGO = null;
        }



       
        if (buildMode)
        {
            HandleBuildMode();
        }

        if (turrets.Count > 0)
        {
            foreach (TurretManager turret in turrets)
            {
                turret.OnUpdate();
            }
        }
    }

   

    private void HandleBuildMode()
    {
        if (buildModeHoverGO == null)
        {
            buildModeHoverGO = Instantiate(buildModeHoverPrefab);
            Vector3 targetScale = Vector3.one * mapGridManager.GetCellSize();
            targetScale.z = 1f;
            buildModeHoverGO.transform.localScale = targetScale;  
        }

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print(mousePos);

        worldPos.z = 0;

        

        Vector2Int gridPos = mapGridManager.WorldPositiontoGridIndex(worldPos); //get grid position of mouse
        Vector2 gridCenter = mapGridManager.GridIndexToGridCenter(gridPos.x, gridPos.y);
        
        if (gridCenter != Vector2.zero)
        {
            currentHoveredGridCenter = gridCenter;
        }
        

        buildModeHoverGO.transform.position = currentHoveredGridCenter;
       
    }

   
    
    #endregion

    #region Class Functions
    public void PlaceBuilding()
    {
        Vector2Int gridpos = mapGridManager.WorldPositiontoGridIndex(currentHoveredGridCenter);
        if (mapGridManager.CheckGridTaken(gridpos))
        {
            Debug.Log("Grid Square is taken");
            return;
        }
        


        float cellSize = mapGridManager.GetCellSize();

        Vector3 targetPos = currentHoveredGridCenter;
     
     
        GameObject turretGO = Instantiate(turretPrefab, targetPos, Quaternion.identity);

        ScaleBuildingToGrid(turretGO, cellSize);

        SetUpTurret(turretGO.GetComponent<TurretManager>());

        //mark square as turret
        mapGridManager.SetGridTurret(gridpos);
    }

    private void ScaleBuildingToGrid(GameObject builtGO, float cellSize)
    {
        

        Vector3 newScale = Vector3.one * cellSize;
        builtGO.transform.localScale = newScale;

    }

    private void SetUpTurret(TurretManager turret)
    {
        turret.OnAwake();
        turret.OnStart();

        turrets.Add(turret);
    }
    #endregion
}
