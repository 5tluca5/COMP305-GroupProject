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

    Vector3 lastPos;

    protected override void SetupTank()
    {
        stat = GameManager.Instance.GetEnemyTankStat(type);

        fireRate = 1 / (stat.fireRate * 0.1f);
        rotationSpeed = 0.2f;

        switch (type)
        {
            case EnemyTankType.Normal:
                changeDirectionTime = 1f;
                break;
            case EnemyTankType.Elite:
                changeDirectionTime = 0.5f;
                break;
            case EnemyTankType.Boss:
                changeDirectionTime = 0.2f;
                break;
        }
    }

    protected override void BeingHit(ProjectileData data)
    {
        if (data.isPlayer)
        {
            stat.health -= data.damage;
        }

        if (stat.health <= 0f)
        {
            //Do explosion?
            
            DropCoin();
            Destroy(gameObject);
        }
    }

    private void DropCoin()
    {
        int amount = 0;

        switch(type)
        {
            case EnemyTankType.Normal:
                amount = Random.Range(10, 50);
                break;
            case EnemyTankType.Elite:
                amount = Random.Range(100, 200);
                break;
            case EnemyTankType.Boss:
                amount = Random.Range(500, 700);
                break;
        }

        GameManager.Instance.EnemyDropCoin(amount);
        GameObject.FindGameObjectWithTag("GUI").GetComponent<GameSceneUI>().CreateEnemyDropCoinGO(transform.position, amount);
    }

    protected override void Start()
    {
        base.Start();

        playerBase = GameObject.FindGameObjectWithTag("playerBase").transform;
        lastPos = transform.position;
    }

    protected override void Update()
    {
        if (!isSpawned) return;
        if (GameManager.Instance.IsGameOver.Value || GameManager.Instance.IsGameClear.Value) return;

        base.Update();

        switch (type)
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
        this.onDestroy = onDestroy;
        isSpawned = true;
        DoRotation(Direction.Down);
    }

    void HandleNormalAI()
    {
        if (fireTimer > fireRate)
        {
            if (Random.Range(0, 3) % 2 == 1 || IsfacingObstacle())
            {
                Fire();
            }
        }

        //if (IsfacingObstacle() || IsfacingWall() || lastDirection == Direction.Down)
        //{
        //    changeDirectionTimer += Time.deltaTime;
        //}

        //if (IsfacingObstacle() || IsfacingWall() || changeDirectionTimer >= changeDirectionTime)
        //{
        //    lastDirection = (Direction)Random.Range((int)Direction.Left, (int)Direction.None);
        //    DoRotation(lastDirection);
        //    changeDirectionTimer = 0;
        //}
        if(IsfacingWall())
        {
            changeDirectionTimer += Time.deltaTime * 2f;
        }
        else if(IsfacingObstacle())
        {
            changeDirectionTimer += Time.deltaTime * 0.5f;
        }

        
        if (changeDirectionTimer >= changeDirectionTime)
        {
            Direction newDir;
            do
            {
                newDir = (Direction)Random.Range((int)Direction.Left, (int)Direction.None);
            } while (newDir == lastDirection);

            lastDirection = newDir;
            DoRotation(lastDirection);
            changeDirectionTimer = 0;
        }
        else if(!IsfacingObstacle() && !IsfacingWall())
        {
            changeDirectionTimer += Time.deltaTime * 0.3f;
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
            if (Random.Range(0, 3) < 2 || IsfacingObstacle())
            {
                Fire();
            }
        }


        if (IsfacingWall())
        {
            changeDirectionTimer += Time.deltaTime * 3f;
        }
        else if (IsfacingObstacle())
        {
            changeDirectionTimer += Time.deltaTime * 0.2f;
        }

        if (changeDirectionTimer >= changeDirectionTime)
        {
            //if (IsfacingWall())
            //{
            //    lastDirection = (Direction)Random.Range((int)Direction.Left, (int)Direction.Down);
            //}
            //else
            //{
            //    if (Random.Range(0, 10) < 5)
            //    {
            //        if (playerBase.position.y + 5 < transform.position.y)
            //        {
            //            lastDirection = Direction.Down;
            //        }
            //        else
            //        {
            //            lastDirection = Direction.Up;
            //        }
            //    }
            //    else if(playerBase.position.x - 5 > transform.position.x)
            //    {
            //        lastDirection = Direction.Right;
            //    }
            //    else if (playerBase.position.x + 5 < transform.position.x)
            //    {
            //        lastDirection = Direction.Left;
            //    }

            //    DoRotation(lastDirection);
            Direction newDir;
            do
            {
                newDir = (Direction)Random.Range((int)Direction.Left, (int)Direction.None);
            } while (newDir == lastDirection);

            lastDirection = newDir;
            DoRotation(lastDirection);
            changeDirectionTimer = 0;
        }
        else if (!IsfacingObstacle() && !IsfacingWall())
        {
            changeDirectionTimer += Time.deltaTime * 0.2f;
            var curRotation = transform.localEulerAngles.z;

            switch (lastDirection)
            {
                case Direction.Left:
                    if (Mathf.Approximately(curRotation, 90f))
                            transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Right:
                    if (Mathf.Approximately(curRotation, 270f))
                            transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Up:
                    if ((curRotation <= 0.001f && curRotation >= -0.001f))
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1));
                    break;
                case Direction.Down:
                    if (Mathf.Approximately(curRotation, 180f))
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
