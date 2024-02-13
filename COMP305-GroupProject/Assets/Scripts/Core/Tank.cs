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

    [Header("Fire")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    protected float fireRate = 1f;
    protected float fireTimer = 0f;

    protected AtlasLoader atlasLoader = AtlasLoader.Instance;

    abstract protected void SetupTank();

    virtual protected void Start()
    {
        SetupTank();
    }

    virtual protected void Fire()
    {
        if (fireTimer < fireRate) return;

        var projectile = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();
        projectile.transform.position = firePoint.position;
        projectile.Setup(stat.damage);
        projectile.Shot();
    }
}
