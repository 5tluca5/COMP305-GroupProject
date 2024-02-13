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
        _enemySpawnerManager = EnemySpawnerManager.Instance;
        LoadData();
    }

    Dictionary<TankParts, TankPart> currentTankParts = new Dictionary<TankParts, TankPart>();

    private EnemySpawnerManager _enemySpawnerManager;

    private void Start()
    {

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

    public void SpawnEnemyTank() 
    {
        _enemySpawnerManager.SpawnEnemyTank();
    }
    public void SpawnMiniBossTank()
    {
        _enemySpawnerManager.SpawnMiniBossTank();
    }
    public void SpawnBossTank()
    {
        _enemySpawnerManager.SpawnBossTank();
    }
    #endregion
}
