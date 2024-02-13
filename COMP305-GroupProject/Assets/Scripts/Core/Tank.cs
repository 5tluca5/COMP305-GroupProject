using DG.Tweening;
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
    [SerializeField] protected float rotationSpeed = 0.2f;
    protected float isRotating = 0f;
    protected Direction lastDirection = Direction.Up;

    [Header("Fire")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] protected Transform firePoint;
    protected float fireRate = 1f;
    protected float fireTimer = 0f;


    protected AtlasLoader atlasLoader = AtlasLoader.Instance;

    abstract protected void SetupTank();

    virtual protected void Start()
    {
        SetupTank();
    }

    virtual protected void Update()
    {
        isRotating -= Time.deltaTime;
    }

    virtual protected void Fire()
    {

        if (fireTimer < fireRate) return;

        var curRotation = transform.localEulerAngles.z;

        if (isRotating <= 0)
        {
            var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = firePoint.position;
            projectile.transform.localScale *= transform.localScale.x;
            projectile.transform.rotation = transform.rotation;
            projectile.Setup(stat.damage);
            projectile.Shot(lastDirection == Direction.Left ? Vector2.left : lastDirection == Direction.Down ? Vector2.down : lastDirection == Direction.Right ? Vector2.right : Vector2.up);

            fireTimer = 0f;
        }

    }

    virtual protected void DoRotation(Direction direction)
    {
        var curRotation = transform.localEulerAngles.z;


        switch (direction)
        {
            case Direction.Left:
                isRotating = curRotation == 270 ? rotationSpeed * 2 : rotationSpeed;
                transform.DORotate(new Vector3(0, 0, 90), curRotation == 270 ? rotationSpeed * 2 : rotationSpeed);
                break;
            case Direction.Right:
                isRotating = curRotation == 90 ? rotationSpeed * 2 : rotationSpeed;
                transform.DORotate(new Vector3(0, 0, -90), curRotation == 90 ? rotationSpeed * 2 : rotationSpeed);
                break;
            case Direction.Up:
                isRotating = curRotation == 180 ? rotationSpeed * 2 : rotationSpeed;
                transform.DORotate(new Vector3(0, 0, 0), curRotation == 180 ? rotationSpeed * 2 : rotationSpeed);
                break;
            case Direction.Down:
                isRotating = curRotation == 0 ? rotationSpeed * 2 : rotationSpeed;
                transform.DORotate(new Vector3(0, 0, -180), curRotation == 0 ? rotationSpeed * 2 : rotationSpeed);
                break;
        }

        isRotating *= 0.9f;
        lastDirection = direction;
    }
}
