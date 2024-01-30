using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

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

        var tempDict = new Dictionary<int, TankPart>();

        var globalId = 1000;

        // Total track: 4
        string[] trackSpriteNames = { "TrackAFrame2", "TrackBFrame2", "TrackСFrame2", "TrackDFrame2"};
        TankStat[] trackStats = { new TankStat(0, 0, 15), new TankStat(0, 0, 10), new TankStat(0, 0, 8), new TankStat(0, 0, 5) };

        for (int i = 0; i < trackSpriteNames.Length; i++)
        {
            var id = globalId + i;
            var tp = new TankPart(id, i, TankParts.Track, trackStats[i], trackSpriteNames[i]);
            tempDict.Add(i, tp);
            dictById.Add(id, tp);
        }

        dictByTankParts.Add(TankParts.Track, tempDict);

        globalId += 1000;
        tempDict = new Dictionary<int, TankPart>();

        // Total Hull: 10
        string[] hullSpriteNames = { "HeavyHullA", "HeavyHullB", "HeavyHullC", "HeavyHullD", "MediumHullA", "MediumHullB", "MediumHullC", "SmallHullA", "SmallHullB", "SmallHullC" };


        for(int i=0; i<hullSpriteNames.Length; i++)
        {
            var id = globalId + i;
            var tp = new TankPart(id, i, TankParts.Hull, new TankStat(10, 1, 3), hullSpriteNames[i]);
            tempDict.Add(i, tp);
            dictById.Add(id, tp);
        }

        dictByTankParts.Add(TankParts.Hull, tempDict);

        tempDict = new Dictionary<int, TankPart>();
        globalId += 1000;

        // Total tower: 10
        string[] towerSpriteNames = { "HeavyTowerA", "HeavyTowerB", "HeavyTowerC", "HeavyTowerD", "MediumTowerA", "MediumTowerB", "MediumTowerC", "SmallTowerA", "SmallTowerB", "SmallTowerC" };
        
        for (int i = 0; i < towerSpriteNames.Length; i++)
        {
            var id = globalId + i;
            var tp = new TankPart(id, i, TankParts.Tower, new TankStat(3, 3+i, 3), towerSpriteNames[i]);
            tempDict.Add(i, tp);
            dictById.Add(id, tp);
        }

        dictByTankParts.Add(TankParts.Tower, tempDict);

        tempDict = new Dictionary<int, TankPart>();
        globalId += 1000;

        // Total Gun: 15
        string[] gunSpriteNames = { "HeavyGunA", "HeavyGunB", "HeavyGunC", "HeavyGunD", "HeavyGunE", "HeavyGunF", "HeavyGunG", "HeavyGunH",
            "MediumGunA", "MediumGunB", "MediumGunC", "MediumGunD", "SmallGunA", "SmallGunB", "SmallGunC", };

        // Total gun connector: 6
        string[] connectorSpriteNames = { "GunConnectorA", "GunConnectorB", "GunConnectorC", "GunConnectorD", "GunConnectorE", "GunConnectorF" };

        for (int i = 0; i < gunSpriteNames.Length; i++)
        {
            var id = globalId + i;
            var tp = new TankPart(id, i, TankParts.Gun, new TankStat(18 - i, 3, 3), gunSpriteNames[i], connectorSpriteNames[i % connectorSpriteNames.Length]);
            tempDict.Add(i, tp);
            dictById.Add(id, tp);
        }

        dictByTankParts.Add(TankParts.Gun, tempDict);
        
    }

    public List<TankPart> GetObtainedTankPart(TankParts parts)
    {
        // Do logic here..

        return dictByTankParts[parts].Values.ToList();
    }

    public TankPart GetTankPartData(TankParts parts, int id)
    {
        if (!dictByTankParts.ContainsKey(parts))
            return new TankPart(0000, 0, Color.gray);
        else
            return dictByTankParts[parts][id];
    }

    public Sprite GetTankPartImage(TankParts parts, int id)
    {
        return AtlasLoader.Instance.GetSprite(GetTankPartData(parts, id).spriteName);
    }
}
