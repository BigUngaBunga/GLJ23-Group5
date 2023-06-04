using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] List<Transform> spawnLocations;
    private List<Transform> players;

    [SerializeField] float timeBetweenSpawns;
    float timer;

    /// <summary>
    /// Can be used to spawn enemies from other scripts.
    /// </summary>
    public static EnemyManager SharedInstance;

    public List<GameObject> pooledEnemies;
    [SerializeField] GameObject NormalEnemyPrefab;
    [SerializeField] GameObject SmallEnemyPrefab;
    [SerializeField] GameObject BigEnemyPrefab;
    [SerializeField] int amountToPool;

    void Awake()
    {
        SharedInstance = this;
        timer = timeBetweenSpawns;
        players = new List<Transform>();
    }

    void Start()
    {
        GetPlayers();
        pooledEnemies = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            if (i % 20 == 19)
            {
                tmp = Instantiate(BigEnemyPrefab);
            }
            else if (i % 5 == 4)
            {
                tmp = Instantiate(SmallEnemyPrefab);
            }
            else
            {
                tmp = Instantiate(NormalEnemyPrefab);
            }

            tmp.SetActive(false);
            pooledEnemies.Add(tmp);
            tmp.transform.SetParent(transform);
        }
    }

    private void GetPlayers()
    {
        var playerControllers = FindObjectsOfType<PlayerController>();
        foreach (var playerController in playerControllers)
        {
            if (playerController.transform)
            {
                players.Add(playerController.transform);

            }
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
                EnemyScript script = enemy.GetComponent<EnemyScript>();
                script.Instantiate(spawnLocations[Random.Range(0, spawnLocations.Count)].position, players);
                timer = timeBetweenSpawns;
            }
            else
            {
                timer = timeBetweenSpawns;
            }
        }
    }
}