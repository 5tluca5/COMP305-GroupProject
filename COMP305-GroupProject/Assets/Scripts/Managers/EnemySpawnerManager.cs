using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

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

        GameManager.Instance.SetEnemySpawnerManager(instance);
    }
    #endregion


    /* Spawnpoints are already placed and GameManager can call them, just neeed these:
     * 1 - Assign the enemy tanks to its designated class
     */

    [SerializeField] private GameObject _spawnAnimationPrefab;

    [SerializeField] private Transform[] _enemySpawn;
    [SerializeField] private GameObject[] _enemyTankPrefabs;
    [SerializeField] private GameObject[] _eliteTankPrefabs;
    [SerializeField] private GameObject[] _bossTankPrefabs;

    private List<EnemyTank> _existingEnemyTanks = new List<EnemyTank>();
    private Subject<EnemyTank> onDestroyTank = new Subject<EnemyTank>();

    [SerializeField] bool isEnabled = false;
    [SerializeField] int maxExistingEnemyCount = 3;
    [SerializeField] List<EnemyTankType> enemyList = new List<EnemyTankType>();
    [SerializeField] int currentEnemyIndex = 0;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float spawnTimer = 0f;

    private void Start()
    {
        onDestroyTank.Subscribe(tank =>
        {
            if(_existingEnemyTanks.Contains(tank))
            {
                _existingEnemyTanks.Remove(tank);
            }
        }).AddTo(this);
    }
    public void SetEnabled(bool set) => isEnabled = set;

    public void Setup(List<EnemyTankType> enemyList, int maxExistingEnemyCount = 3)
    {
        _existingEnemyTanks.Clear();
        this.enemyList = enemyList;
        this.maxExistingEnemyCount = maxExistingEnemyCount;
        currentEnemyIndex = 0;
        spawnTimer = 0f;
    }

    private void Update()
    {
        if (!isEnabled) return;

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnInterval)
        {
            SpawnEnemy();

            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if(currentEnemyIndex >= enemyList.Count) return;
        if (_existingEnemyTanks.Count >= maxExistingEnemyCount) return;

        var enemyType = enemyList[currentEnemyIndex];
        var spawnIndex = Random.Range(0, _enemySpawn.Length);
        var spot = _enemySpawn[spawnIndex];

        switch (enemyType)
        {
            case EnemyTankType.Normal:
                StartCoroutine(CreateSpawnEffect(spot, () => { SpawnEnemyTank(spot); }));
                break;
            case EnemyTankType.Elite:
                StartCoroutine(CreateSpawnEffect(spot, () => { SpawnEliteTank(spot); }));
                break;
            case EnemyTankType.Boss:
                StartCoroutine(CreateSpawnEffect(spot, () => { SpawnBossTank(spot); }));
                break;
        }
    }

    //spawning normal enemy tanks
    public void SpawnEnemyTank(Transform spawnSpot)
    {
        var tank = Instantiate(_enemyTankPrefabs[Random.Range(0, _enemyTankPrefabs.Length)], spawnSpot.position, Quaternion.identity).GetComponent<EnemyTank>();
        tank.Spawn(onDestroyTank);
        _existingEnemyTanks.Add(tank);
        currentEnemyIndex++;
    }

    //spawning mini boss every 4th and 5th level
    public void SpawnEliteTank(Transform spawnSpot)
    {
        var tank = Instantiate(_eliteTankPrefabs[Random.Range(0, _eliteTankPrefabs.Length)], spawnSpot.position, Quaternion.identity).GetComponent<EnemyTank>();
        tank.Spawn(onDestroyTank);
        _existingEnemyTanks.Add(tank);
        currentEnemyIndex++;
    }


    //spawning boss every 5th level
    public void SpawnBossTank(Transform spawnSpot)
    {
        var tank = Instantiate(_bossTankPrefabs[Random.Range(0, _bossTankPrefabs.Length)], spawnSpot.position, Quaternion.identity).GetComponent<EnemyTank>();
        tank.Spawn(onDestroyTank);
        _existingEnemyTanks.Add(tank);
        currentEnemyIndex++;
    }

    IEnumerator CreateSpawnEffect(Transform spawnPoint, UnityAction spawnAction)
    {
        Instantiate(_spawnAnimationPrefab, spawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        spawnAction?.Invoke();
    }
}
