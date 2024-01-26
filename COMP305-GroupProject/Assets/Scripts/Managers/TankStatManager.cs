using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TankStatManager : MonoBehaviour
{
    static TankStatManager instance;

    public static TankStatManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new TankStatManager();
            }

            return instance;
        }
    }

    public TankStatManager()
    {
        GenerateData();
    }

    [Header("Current Stat")]
    [SerializeField] ReactiveProperty<TankStat> currentTankStat;
    Dictionary<TankParts, int> currentTankParts = new Dictionary<TankParts, int>();

    [Header("Tank stat setting")]
    Dictionary<TankParts, Dictionary<int, TankPart>> dictByTankParts = new Dictionary<TankParts, Dictionary<int, TankPart>>();
    Dictionary<int, TankPart> dictById = new Dictionary<int, TankPart>();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void LoadData()
    {
        currentTankParts.Clear();


    }

    public void CalculateTankStat()
    {
        var newStat = new TankStat(0, 0, 0);

        

    }
    //Modify the attributes data here
    void GenerateData()
    {
        dictByTankParts.Clear();
        dictById.Clear();

        var globalId = 1000;

        // Total track: 4
        string[] trackSpriteNames = { "TrackAFrame2", "TrackBFrame2", "TrackCFrame2", "TrackDFrame2"};
        TankStat[] trackStats = { new TankStat(0, 0, 15), new TankStat(0, 0, 10), new TankStat(0, 0, 8), new TankStat(0, 0, 5) };

        globalId += 1000;

        // Total Hull: 10
        string[] hullSpriteNames = { "HeavyHullA", "HeavyHullB", "HeavyHullC", "HeavyHullD", "MediumHullA", "MediumHullB", "MediumHullC", "SmallHullA", "SmallHullB", "SmallHullC" };

        var tempDict = new Dictionary<int, TankPart>();

        for(int i=0; i<hullSpriteNames.Length; i++)
        {
            var id = globalId + i;
            var tp = new TankPart(id, i, TankParts.Hull, new TankStat(10, 1, 3), hullSpriteNames[i]);
            tempDict.Add(i, tp);
            dictById.Add(id, tp);
        }

        dictByTankParts.Add(TankParts.Hull, tempDict);

        globalId += 1000;

        // Total tower: 10
        string[] towerSpriteNames = { "HeavyTowerA", "HeavyTowerB", "HeavyTowerC", "HeavyTowerD", "MediumTowerA", "MediumTowerB", "MediumTowerC", "SmallTowerA", "SmallTowerB", "SmallTowerC" };

        globalId += 1000;

        // Total Gun: 15
        string[] gunSpriteNames = { "HeavyGunA", "HeavyGunB", "HeavyGunC", "HeavyGunD", "HeavyGunE", "HeavyGunF", "HeavyGunG", "HeavyGunH",
            "MediumGunA", "MediumGunB", "MediumGunC", "MediumGunD", "SmallGunA", "SmallGunB", "SmallGunC", };

        globalId += 1000;

        // Total gun connector: 6
        string[] connectorSpriteNames = { "GunConnectorA", "GunConnectorB", "GunConnectorC", "GunConnectorD", "GunConnectorE", "GunConnectorF" };
        
    }


}
