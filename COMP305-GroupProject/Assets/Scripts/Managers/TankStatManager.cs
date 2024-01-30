using System;
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
        GenerateGameData();
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
    void GenerateGameData()
    {
        GenerateTankPartStatList();

        dictByTankParts.Clear();
        dictById.Clear();

        //Generate tank part game data from csvlist
        GeneratePartsDataBy(TankParts.Hull);
        GeneratePartsDataBy(TankParts.Tower);
        GeneratePartsDataBy(TankParts.Gun);
        GeneratePartsDataBy(TankParts.Track);
    }


    [Serializable]
    private class TankPartStatData
    {
        public string partName;
        public string type;
        public float damage;
        public int health;
        public float fireRate;
        public float movementSpeed;
        public int price;
    }

    private TextAsset tankPartStatCSVtData;
    private List<TankPartStatData> tankPartStatList = new List<TankPartStatData>();


    void GenerateTankPartStatList()
    {
        dictByTankParts.Clear();
        dictById.Clear();
        var globalId = 1000;
        tankPartStatCSVtData = Resources.Load<TextAsset>("TankStat1");

        // get data from csv
        string[] data = tankPartStatCSVtData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        //get the table size and create list with the given size
        int tableSize = data.Length / 7 - 1;

        for (int i = 0; i < tableSize; i++)
        {
            var tempTankPart = new TankPartStatData();
            tempTankPart.partName = data[7 * (i + 1)];
            tempTankPart.type = data[7 * (i + 1) + 1];
            tempTankPart.damage = float.Parse(data[7 * (i + 1) + 2]);
            tempTankPart.health = int.Parse(data[7 * (i + 1) + 3]);
            tempTankPart.fireRate = float.Parse(data[7 * (i + 1) + 4]);
            tempTankPart.movementSpeed = float.Parse(data[7 * (i + 1) + 5]);
            tempTankPart.price = int.Parse(data[7 * (i + 1) + 6]);
            tankPartStatList.Add(tempTankPart);
        }
    }

    void GeneratePartsDataBy(TankParts type) 
    {
        string tankPart;
        int globalId = 0;

        switch (type) 
        {
            case TankParts.Hull:
                tankPart = "Hull";
                globalId = 1000;
                break;
            case TankParts.Tower:
                tankPart = "Tower";
                globalId = 2000;
                break;
            case TankParts.Gun:
                tankPart = "Gun";
                globalId = 3000;
                break;
            case TankParts.Track:
                tankPart = "TrackFrame";
                globalId = 4000;
                break;
        }

        var tempDictPart = new Dictionary<int, TankPart>();
        List<TankPartStatData> parts = new List<TankPartStatData>();
        parts = tankPartStatList.FindAll(x => x.type == "Hull");
        int j = 0;
        foreach (var part in parts)
        {
            var id = globalId + j;
            var tp = new TankPart(id, j, type, new TankStat(part.damage, part.fireRate, part.movementSpeed), part.partName);
            tempDictPart.Add(j, tp);
            dictById.Add(id, tp);
            j++;
        }

        dictByTankParts.Add(type, tempDictPart);
    }


}