using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tank : MonoBehaviour
{
    [Header("Tank Part Images")]
    [SerializeField] protected SpriteRenderer lightImg;
    [SerializeField] protected SpriteRenderer trackLImg;
    [SerializeField] protected SpriteRenderer trackRImg;
    [SerializeField] protected SpriteRenderer hullImg;
    [SerializeField] protected SpriteRenderer towerImg;
    [SerializeField] protected SpriteRenderer gunConnectorImg;
    [SerializeField] protected SpriteRenderer gunImg;

    [Header("Tank Stat")]
    [SerializeField] protected TankStat stat;

    protected AtlasLoader atlasLoader = AtlasLoader.Instance;

    abstract protected void SetupTank();

    virtual protected void Start()
    {
        SetupTank();
    }
}
