using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        AtlasLoader.Instance.LoadAtlas("Artworks/TankConstructor/Images");
        Debug.Log("Atlas Count: " + AtlasLoader.Instance.AtlasCount());

        TankStatManager.Instance.Init();
        LoadData();
    }

    Dictionary<TankParts, TankPart> currentTankParts = new Dictionary<TankParts, TankPart>();

    private EnemySpawnerManager _enemySpawnerManager;

    public void SetEnemySpawnerManager(EnemySpawnerManager esm)
    {
        _enemySpawnerManager = esm;
        var testData = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        _enemySpawnerManager.Setup(testData.Select(x => (EnemyTankType)x).ToList(), 3);
        _enemySpawnerManager.SetEnabled(true);
    }

    private void Start()
    {
        //var testData = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        //_enemySpawnerManager.Setup(testData.Select(x => (EnemyTankType)x).ToList(), 3);
        //_enemySpawnerManager.SetEnabled(true);
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
                return new TankStat(30, 3, 3, 30);
            case EnemyTankType.Elite:
                return new TankStat(90, 5, 4, 100);
            case EnemyTankType.Boss:
                return new TankStat(150, 10, 2, 300);
        }

        return new TankStat(1, 5, 10, 3);
    }
}
