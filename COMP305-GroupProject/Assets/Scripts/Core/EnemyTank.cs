using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTankType
{
    Normal,
    Elite,
    Boss
}

public class EnemyTank : Tank
{
    [SerializeField] EnemyTankType type = EnemyTankType.Normal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void SetupTank()
    {
        stat = GameManager.Instance.GetEnemyTankStat(type);

        fireRate = 1 / (stat.fireRate * 0.1f);
        rotationSpeed = 0.2f;
    }
}
