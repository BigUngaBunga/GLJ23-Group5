using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] List<Transform> spawnLocations;
    
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float timeBetweenWaves;
    float timeBetweenSpawnsInWave;
    float spawnTimer, waveTimer;

    /// <summary>
    /// Can be used to spawn enemies from other scripts.
    /// </summary>
    public static EnemyManager SharedInstance;

    [SerializeField] GameObject NormalEnemyPrefab;
    [SerializeField] GameObject SmallEnemyPrefab;
    [SerializeField] GameObject LargeEnemyPrefab;

    int wave, currentSpawn, numOfSpawnsInWave;
    [SerializeField] int startLargeSpawnIntervall = 10, startSmallSpawnIntervall=8;
    bool spawnWave;
    Transform waveSpawnLocation;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(6, 8);

        SharedInstance = this;
        timeBetweenSpawnsInWave = 0.05f;
    }

    void Start()
    {
        numOfSpawnsInWave = 3;
        NewWave();
    }

    public void NewWave()
    {
        if (wave % 2 == 1)
        {
            if (startLargeSpawnIntervall > 5)
                startLargeSpawnIntervall--;

            if (startSmallSpawnIntervall > 3)
                startSmallSpawnIntervall--;
        }

        wave++;
        numOfSpawnsInWave += 2;
        currentSpawn = 0;

        waveSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];

        spawnWave = true;
        spawnTimer = 0;
        waveTimer = timeBetweenWaves;
    }
    

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnWave)
        {
            if (spawnTimer <= 0)
            {
                currentSpawn++;
                GameObject enemy;

                if (currentSpawn % startLargeSpawnIntervall == 0)
                {
                    enemy = Instantiate(LargeEnemyPrefab);
                }
                else if (currentSpawn % startSmallSpawnIntervall == 0)
                {
                    enemy = Instantiate(SmallEnemyPrefab);
                }
                else
                {
                    enemy = Instantiate(NormalEnemyPrefab);
                }
                
                EnemyScript script = enemy.GetComponent<EnemyScript>();
                script.Initiate(waveSpawnLocation.position);
                spawnTimer = timeBetweenSpawnsInWave;

                if(currentSpawn>=numOfSpawnsInWave)
                {
                    spawnWave = false;
                    spawnTimer = timeBetweenSpawns;
                }
            }
        }
        else
        {
            if (spawnTimer <= 0)
            {
                GameObject enemy = Instantiate(NormalEnemyPrefab);
                
                EnemyScript script = enemy.GetComponent<EnemyScript>();
                script.Initiate(spawnLocations[Random.Range(0, spawnLocations.Count)].position);
                spawnTimer = timeBetweenSpawns;
            }

            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                NewWave();
            }
        }        
    }
}