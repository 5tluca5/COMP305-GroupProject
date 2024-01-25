using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPartManager : MonoBehaviour
{
    static TankPartManager instance;

    public static TankPartManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new TankPartManager();
            }

            return instance;
        }
    }

    public TankPartManager()
    {
        GenerateData();
    }

    Dictionary<TankParts, TankPart> dictByTankParts = new Dictionary<TankParts, TankPart>();


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void GenerateData()
    {
        // Total track: 4
        string[] trackSpriteNames = { };
    }
}
