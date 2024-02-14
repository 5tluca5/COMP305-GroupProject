using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public ReactiveProperty<bool> IsGameOver { get; private set; } = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsGameClear { get; private set; } = new ReactiveProperty<bool>(false);
    public ReactiveProperty<int> CurrentGameLevel { get; private set; } = new ReactiveProperty<int>(1);
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void SetGameOver(bool set) => IsGameOver.Value = set;
    public void SetGameClear(bool set) => IsGameClear.Value = set;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        AtlasLoader.Instance.LoadAtlas("Artworks/TankConstructor/Images");
        Debug.Log("Atlas Count: " + AtlasLoader.Instance.AtlasCount());

        TankStatManager.Instance.Init();
        LoadData();

        IsGameClear.Subscribe(x =>
        {
            if(_enemySpawnerManager)
                _enemySpawnerManager.SetEnabled(false);
        }).AddTo(this);

        IsGameOver.Subscribe(x =>
        {
            if(_enemySpawnerManager)
                _enemySpawnerManager.SetEnabled(false);
        }).AddTo(this);
    }

    Dictionary<TankParts, TankPart> currentTankParts = new Dictionary<TankParts, TankPart>();

    private EnemySpawnerManager _enemySpawnerManager;
    Dictionary<int, List<int>> levelDataDict = new Dictionary<int, List<int>>();

    public void SetEnemySpawnerManager(EnemySpawnerManager esm)
    {
        _enemySpawnerManager = esm;
        _enemySpawnerManager.Setup(levelDataDict[CurrentGameLevel.Value].Select(x => (EnemyTankType)x).ToList(), 3);
        _enemySpawnerManager.SetEnabled(true);
    }

    private void Start()
    {
        GenerateLevelData();
    }

    void GenerateLevelData()
    {
        levelDataDict.Clear();

        levelDataDict.Add(1, new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 });
        levelDataDict.Add(2, new List<int> { 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 });
        levelDataDict.Add(3, new List<int> { 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 });
        levelDataDict.Add(4, new List<int> { 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 });
        levelDataDict.Add(5, new List<int> { 0, 1, 1, 0, 1, 0, 1, 0, 0, 1 });
        levelDataDict.Add(6, new List<int> { 0, 0, 0, 0, 1, 0, 0, 1, 1, 1 });
        levelDataDict.Add(7, new List<int> { 1, 1, 1, 0, 1, 0, 0, 1, 0, 1 });
        levelDataDict.Add(8, new List<int> { 1, 0, 1, 0, 1, 0, 1, 0, 0, 1 });
        levelDataDict.Add(9, new List<int> { 1, 0, 1, 0, 1, 0, 0, 1, 0, 1 });
        levelDataDict.Add(10, new List<int> { 1, 0, 1, 0, 1, 0, 0, 1, 0, 1 });

        for (int i = 11; i < 100; i++)
        {
            int totalEnemy = 10 + (i / 10) * 2;
            List<int> data = new List<int>();

            for (int j = 0; j < totalEnemy; j++)
            {
                data.Add(UnityEngine.Random.Range(0, 100) < i ? 1 : 0);
            }

            levelDataDict.Add(i, data);
        }
    }

    void LoadData()
    {
        currentTankParts.Clear();

        var tsm = TankStatManager.Instance;

        foreach(TankParts tp in Enum.GetValues(typeof(TankParts)))
        {
            var tpId = PlayerPrefs.GetInt(Constant.SAVE_KEY_CURRENT_TANK_PART + tp.ToString(), 0);
            currentTankParts.Add(tp, tsm.GetTankPartData(tp, tpId));
        }
    }

    public Dictionary<TankParts, TankPart> GetCurrentTankParts()
    {
        return currentTankParts;
    }

    public void SetCurrentTankParts(List<TankPart> tps)
    {
        foreach(var tp in tps)
        {
            currentTankParts[tp.parts] = tp;
        }
    }

    public TankStat GetCurrentTankStat()
    {
        return TankStatManager.Instance.CalculateTankStat(currentTankParts.Values.ToList());
    }

    #region - Enemy Spawn

    //public void SpawnEnemyTank() 
    //{
    //    _enemySpawnerManager.SpawnEnemyTank();
    //}
    //public void SpawnMiniBossTank()
    //{
    //    _enemySpawnerManager.SpawnMiniBossTank();
    //}
    //public void SpawnBossTank()
    //{
    //    _enemySpawnerManager.SpawnBossTank();
    //}
    #endregion

    public TankStat GetEnemyTankStat(EnemyTankType type)
    {
        switch(type)
        {
            case EnemyTankType.Normal:
                return new TankStat(30, 3, 3, 100);
            case EnemyTankType.Elite:
                return new TankStat(90, 5, 4, 400);
            case EnemyTankType.Boss:
                return new TankStat(150, 10, 2, 1000);
        }

        return new TankStat(1, 5, 10, 3);
    }

    public void GoToNextLevel()
    {
        CurrentGameLevel.Value++;

        _enemySpawnerManager.Setup(levelDataDict[CurrentGameLevel.Value].Select(x => (EnemyTankType)x).ToList(), 3);
        _enemySpawnerManager.SetEnabled(true);
    }
}
