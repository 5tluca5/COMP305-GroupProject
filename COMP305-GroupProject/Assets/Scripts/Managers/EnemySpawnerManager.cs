using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    #region - Singleton Code
    static EnemySpawnerManager instance;

    public static EnemySpawnerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemySpawnerManager();
            }

            return instance;
        }
    }

    public EnemySpawnerManager()
    {
        instance = this;
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    /* Spawnpoints are already placed and GameManager can call them, just neeed these:
     * 1 - Assign the enemy tanks to its designated class
     */

    [SerializeField]
    private Transform[] _enemySpawn;

    [SerializeField]
    private GameObject[] _enemyTanks;

    [SerializeField]
    private GameObject[] _miniBossTanks;

    [SerializeField]
    private GameObject[] _bossTanks;

    //spawning normal enemy tanks
    public void SpawnEnemyTank() 
    {
        var spawnIndex = Random.Range(0, _enemySpawn.Length - 1);
        Instantiate(_enemyTanks[Random.Range(0, _enemyTanks.Length - 1)], _enemySpawn[spawnIndex].position, Quaternion.identity);
    }

    //spawning mini boss every 4th and 5th level
    public void SpawnMiniBossTank()
    {
        var spawnIndex = Random.Range(0, _enemySpawn.Length - 1);
        Instantiate(_miniBossTanks[Random.Range(0, _miniBossTanks.Length - 1)], _enemySpawn[spawnIndex].position, Quaternion.identity);
    }


    //spawning boss every 5th level
    public void SpawnBossTank()
    {
        var spawnIndex = Random.Range(0, _enemySpawn.Length - 1);
         Instantiate(_bossTanks[Random.Range(0, _bossTanks.Length - 1)], _enemySpawn[spawnIndex].position, Quaternion.identity);
    }
}
