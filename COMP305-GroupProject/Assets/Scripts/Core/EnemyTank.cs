using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum EnemyTankType : int
{
    Normal = 0,
    Elite,
    Boss
}

public class EnemyTank : Tank
{
    [SerializeField] EnemyTankType type = EnemyTankType.Normal;

    private bool isSpawned = false;

    private float changeDirectionTimer = 0f;
    private float changeDirectionTime = 0f;

    private Transform playerBase;

    private Subject<EnemyTank> onDestroy;

    protected override void SetupTank()
    {
        stat = GameManager.Instance.GetEnemyTankStat(type);

        fireRate = 1 / (stat.fireRate * 0.1f);
        rotationSpeed = 0.2f;

        switch(type)
        {
                case EnemyTankType.Normal:
                changeDirectionTime = 1f;
                break;
                case EnemyTankType.Elite:
                changeDirectionTime = 1f;
                break;
                case EnemyTankType.Boss:
                changeDirectionTime = 0.2f;
                break;
        }
    }

    protected override void BeingHit(ProjectileData data)
    {
        if(data.isPlayer)
        {
            stat.health -= data.damage;
        }

        if(stat.health <= 0f)
        {
            //Do explosion?

            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();

        playerBase = GameObject.FindGameObjectWithTag("playerBase").transform;
        //Debug
        Spawn(null);
    }

    protected override void Update()
    {
        if (!isSpawned) return;

        base.Update();

        switch(type)
        {
            case EnemyTankType.Normal:
                HandleNormalAI();
                break;
            case EnemyTankType.Elite:
                HandleEliteAI();
                break;
            case EnemyTankType.Boss: 
                break;
        }
    }

    public void Spawn(Subject<EnemyTank> onDestroy)
    {
        isSpawned = true;

        lastDirection = Direction.Down;
    }

    void HandleNormalAI()
    {
        if(fireTimer > fireRate)
        {
            if(Random.Range(0, 3) %2 == 1)
            {
                Fire();
            }
        }

        if(IsfacingObstacle() || IsfacingWall() || lastDirection == Direction.Down)
        {
            changeDirectionTimer += Time.deltaTime;

            if (changeDirectionTimer >= changeDirectionTime)
            {
                lastDirection = (Direction)Random.Range((int)Direction.Left, (int)Direction.None);
                DoRotation(lastDirection);
                changeDirectionTimer = 0;
            }
        }
        else
        {
            var curRotation = transform.localEulerAngles.z;

            switch (lastDirection)
            {
                case Direction.Left:
                    if (curRotation == 90)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Right:
                    if (curRotation == 270)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Up:
                    if ((curRotation <= 0.001 && curRotation >= -0.001))
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Down:
                    if (curRotation == 180)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
            }
        }
    }

    void HandleEliteAI()
    {
        if (fireTimer > fireRate)
        {
            if (Random.Range(0, 3) < 2)
            {
                Fire();
            }
        }

        changeDirectionTimer += Time.deltaTime;

        if (IsfacingWall() || changeDirectionTimer >= changeDirectionTime)
        {
            if(IsfacingWall())
            {
                lastDirection = (Direction)Random.Range((int)Direction.Left, (int)Direction.Down);
            }
            else
            {
                lastDirection = Random.Range(0, 10) <= 3 ? Direction.Down : (Direction)Random.Range((int)Direction.Left, (int)Direction.Down);
            }

            DoRotation(lastDirection);

            changeDirectionTimer = 0;
        }
        else if (!IsfacingObstacle())
        {
            var curRotation = transform.localEulerAngles.z;

            switch (lastDirection)
            {
                case Direction.Left:
                    if (curRotation == 90)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Right:
                    if (curRotation == 270)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Up:
                    if ((curRotation <= 0.001 && curRotation >= -0.001))
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Down:
                    if (curRotation == 180)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        onDestroy?.OnNext(this);
    }
}
