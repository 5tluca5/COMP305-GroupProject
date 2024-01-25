using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Start()
    {
        AtlasLoader.Instance.LoadAtlas("Artworks/TankConstructor/Images");
        Debug.Log("Atlas Count: " + AtlasLoader.Instance.AtlasCount());

    }
}
