using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] List<Transform> spawnLocations;
    [SerializeField] List<Transform> playerPositions;

    [SerializeField] float timeBetweenSpawns;
    float timer;
    [SerializeField] float distanceToPlayers;

    /// <summary>
    /// Can be used to spawn enemies from other scripts.
    /// </summary>
    public static EnemyManager SharedInstance;

    public List<GameObject> pooledEnemies;
    [SerializeField] GameObject EnemyPrefab;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
        timer = timeBetweenSpawns;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledEnemies = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(EnemyPrefab);
            tmp.SetActive(false);
            pooledEnemies.Add(tmp);
            tmp.transform.SetParent(this.transform);
        }
    }

    /// <summary>
    /// Called to return an enemy to spawn. No that the enemy script's Instanciate method has to be called as well 
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject enemy = GetPooledObject();
            if (enemy != null)
            {
                List<Transform> potentialSpawnLocations = new List<Transform>();

                //checks which spawnlocations are too close to the player(s)
                for (int i = 0; i < spawnLocations.Count; i++)
                {
                    bool add = true;

                    for (int j = 0; j < playerPositions.Count; j++)
                    {
                        if (Vector3.Distance(playerPositions[j].position, spawnLocations[i].position) < distanceToPlayers)
                        {
                            add = false;
                        }
                    }

                    if (add)
                    {
                        potentialSpawnLocations.Add(spawnLocations[i]);
                    }
                }

                if (potentialSpawnLocations.Count != 0)
                {
                    EnemyScript script = enemy.GetComponent<EnemyScript>();
                    script.Instanciate(potentialSpawnLocations[Random.Range(0, potentialSpawnLocations.Count)].position, playerPositions);
                    timer = timeBetweenSpawns;
                }
                else
                {
                    timer = timeBetweenSpawns / 2;
                }
            }
        }
    }
}