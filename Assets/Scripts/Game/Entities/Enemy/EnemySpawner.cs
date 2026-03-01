using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    #region Class References
    GameManager gameManager;

    PlayerManager playerManager;
    MapGridManager mapGridManager;
    EnemyManager enemyManager;
    WaveBar waveBar;
    [SerializeField] Transform player;


    #endregion

    #region Private Fields
    [Header("Enemy Fields")]

    [SerializeField] private List<EnemyType> enemyTypes = new List<EnemyType>();


    [SerializeField] private List<EnemyManager> enemies = new List<EnemyManager>();

    [SerializeField] private float minSpawnRadius = 3f;
    [SerializeField] private float maxSpawnRadius = 5f;
    [SerializeField] private int waveInterval = 250;   //frames
    [SerializeField] private int enemyInterval = 50;   //frames

    private int waveIntervalCounter;
    [SerializeField]private int waveNo = 0;
    private int waveEnemies = 1;
    private int EnemiesDestroyed = 0;
    
    private int i = 0;
    private bool interval = true;      //game phase between waves
    private bool spawning = false;       //game phase of spawning enemies
    private int frame = 0;                 //frame counter (can use something global)

    private static EnemySpawner _instance;
    #endregion

    #region Properties
    public static EnemySpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<EnemySpawner>();
                if (_instance == null)
                {
                    Debug.LogError("EnemySpwaner has not been assigned");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Start Up
    public void OnAwake()
    {
        playerManager = PlayerManager.Instance;
        player = PlayerManager.Instance.transform;
        gameManager = GameManager.Instance;
        waveBar = WaveBar.Instance;
        mapGridManager = FindAnyObjectByType<MapGridManager>();
    }
    
    public void OnStart()
    {
        int waveIntervalCounter = waveInterval;
       
    }
    #endregion

    #region Class Functions
    public void OnFixedUpdate()
    {
        if (waveNo == 11)
        {
            //finish game
            gameManager.CompleteLevel();
        }

        if (interval)      //game phase between waves
        {

            if (waveIntervalCounter <= 0)
            {
                waveEnemies = waveNo + 1;   //formula for enemies per wave
                waveIntervalCounter = waveInterval;        //reset 10s interval between waves
                frame = 0;                  //frame counter reset to be ready for enemy spawning code
                spawning = true;
                interval = false;
                EnemiesDestroyed = 0; //reset count of kill
                Debug.Log("Wave interval ended, initiating enemy spawning");
                waveBar.SetWave();
            }
            else
            {
                waveIntervalCounter--;
            }
        }

        if (spawning)       //game phase of spawning enemies
        {
            frame++;
            if (frame >= enemyInterval)   //spawn enemies every 3 seconds in the spawning phase
            {
                
                if (i < waveEnemies)
                {
                    //spawn an enemy
                    float randomAngle = Random.Range(0, 2 * Mathf.PI);
                    float randomRadius = Random.Range(minSpawnRadius, maxSpawnRadius);            //random distance and angle from the player for polar coordinate of spawning location

                    Vector3 targetPosition = new Vector3(randomRadius * Mathf.Cos(randomAngle) + player.transform.position.x, randomRadius *Mathf.Sin(randomAngle) + player.transform.position.y, 0);

                    Vector2Int gridPos = mapGridManager.WorldPositiontoGridIndex(targetPosition);

                    if (!mapGridManager.CheckOutOfBounds(gridPos))
                    {
                        if (!mapGridManager.CheckGridTaken(gridPos))
                        {
                            frame = 0;
                            GameObject enemyPrefab = ChooseEnemy();

                            GameObject newEnemy = Instantiate(enemyPrefab, targetPosition, Quaternion.identity);
                            //GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(5,5,0), Quaternion.identity);
                            enemies.Add(newEnemy.GetComponent<EnemyManager>());

                            ProcessNewEnemy(newEnemy.GetComponent<EnemyManager>());


                            if (i % 5 == 0) //elite enemy every 5
                            {

                            }
                            else
                            {

                            }
                            i++;
                        }
                        else
                        {
                            Debug.Log("Failed to spawn enemy, Grid Position taken");
                        }
                    }
                    else
                    {
                        Debug.Log("Failed to spawn enemy, out of bounds");
                    }
                    
                    
                }
                if (i >= waveEnemies)
                {
                    frame = 0;

                    spawning = false;
                    Debug.Log("Spawning finished, wave will end when all enemies are defeated");
                    waveNo++;
                    i = 0;
                    if (waveNo % 5 == 0)
                    {
                        //give turret player
                        playerManager.IncreaseTurretCount();
                    }
                }
            }
        }



        if (!spawning && !interval && enemies.Count == 0) //requires enemy count to be input from other classes, otherwise it will always be 0
        {
            interval = true;
            playerManager.GainLevel();
            Debug.Log("Initiating wave interval");
        }
    }

    public GameObject ChooseEnemy()
    {
        float tWeight = 0;

        foreach (EnemyType enemyType in enemyTypes)
        {
            float spawnChanceWithWave = enemyType.spawnChance * (waveNo >= 5 ? 1.5f : 0.5f); // calculate a spawn chacen for the enemies, if its past wave 5 make the spawn chance higher
            tWeight += spawnChanceWithWave; // calc a total weight of all the enemies spawn chances
        }

        float randomThreshold = Random.Range(0, tWeight);
        float cWeight = 0;

        foreach (EnemyType enemyType in enemyTypes)
        {
            float spawnChanceWithWave = enemyType.spawnChance * (waveNo >= 5 ? 1.5f : 0.5f);
            cWeight += spawnChanceWithWave;

            if (randomThreshold <= cWeight) 
            {
                return enemyType.enemyPrefab;
            }
        }

        return enemyTypes[0].enemyPrefab;
    }

    private void ProcessNewEnemy(EnemyManager enemy)
    {
        enemy.OnAwake();
        enemy.OnStart();
    }

    public void OnUpdate()
    {

        List<EnemyManager> temp = new List<EnemyManager>();

        foreach(EnemyManager enemy in enemies)
        {
            enemy.OnUpdate();

            if (enemy.IsDead)
            {
                Destroy(enemy.gameObject);
                EnemiesDestroyed++; // counting kills 
            }
            else
            {
                temp.Add(enemy);
            }
        }

        enemies = temp;
    }

    public int GetCurrentWave()
    {
        return waveNo;
    }
    #endregion

    public int GetTotalEnemies()
    {
        return waveEnemies;
    }

    public int EnemyDestroyed()
    {
        return EnemiesDestroyed;
    }
}

[System.Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
    public float spawnChance;
}