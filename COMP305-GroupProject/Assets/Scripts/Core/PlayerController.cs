using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using DG.Tweening;
using UnityEditor;

public class PlayerController : PlayerTank
{
    [Header("Controll Keys")]
    [SerializeField] KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] KeyCode rightKey = KeyCode.RightArrow;
    [SerializeField] KeyCode upKey = KeyCode.UpArrow;
    [SerializeField] KeyCode downKey = KeyCode.DownArrow;
    [SerializeField] KeyCode fireKey = KeyCode.Space;

    [SerializeField] Direction curDireciton = Direction.None;
    Stack<Direction> inputs = new Stack<Direction>();

    private bool blocked = false;

    override protected void Update()
    {
        
        base.Update();

        HandleMovement();
        HandleFire();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wallDetection.position, new Vector3(2.2f, 0.5f));
    }
    private void HandleMovement()
    {
        var left = Input.GetKey(leftKey);
        var right = Input.GetKey(rightKey);
        var up = Input.GetKey(upKey);
        var down = Input.GetKey(downKey);

        var horizontal = Mathf.Abs(Input.GetAxis("Horizontal"));  //Temp disable
        var vertical = Mathf.Abs(Input.GetAxis("Vertical"));  //Temp disable
        //var horizontal = 1;
        //var vertical = 1;

        if (!left && !right && !up && !down)
        {
            curDireciton = Direction.None;
            inputs.Clear();
        }

        if (Input.GetKeyDown(downKey))
        {
            inputs.Push(Direction.Down);
            DoRotation(Direction.Down);
        }
        if (Input.GetKeyDown(upKey))
        {
            inputs.Push(Direction.Up);
            DoRotation(Direction.Up);
        }
        if (Input.GetKeyDown(leftKey))
        {
            inputs.Push(Direction.Left);
            DoRotation(Direction.Left);
        }
        if (Input.GetKeyDown(rightKey))
        {
            inputs.Push(Direction.Right);
            DoRotation(Direction.Right);
        }

        if(inputs.Count > 0)
        {
            if (Input.GetKeyUp(downKey) && inputs.Peek() == Direction.Down)
            {
                inputs.Pop();

                if (inputs.Count > 0)
                    DoRotation(inputs.First());
            }
            if (Input.GetKeyUp(upKey) && inputs.Peek() == Direction.Up)
            {
                inputs.Pop();

                if (inputs.Count > 0)
                    DoRotation(inputs.First());
            }
            if (Input.GetKeyUp(leftKey) && inputs.Peek() == Direction.Left)
            {
                inputs.Pop();

                if (inputs.Count > 0)
                    DoRotation(inputs.First());
            }
            if (Input.GetKeyUp(rightKey) && inputs.Peek() == Direction.Right)
            {
                inputs.Pop();

                if (inputs.Count > 0)
                    DoRotation(inputs.First());
            }
        }
        

        if (!left && !right && !up && !down)
        {
            curDireciton = Direction.None;
            inputs.Clear();
        }
        else
        {
            curDireciton = inputs.Count <= 0 ? Direction.None : inputs.First();
        }

        //PrintDebug();
        //switch (curDireciton)
        //{
        //    case Direction.Left:
        //        if(left && transform.rotation.z == 90f)
        //            transform.Translate(new Vector2(stat.movementSpeed * Time.deltaTime * -1 * 5, 0));
        //        break;
        //    case Direction.Right:
        //        if(right)
        //            transform.Translate(new Vector2(stat.movementSpeed * Time.deltaTime * 1 * 5, 0));
        //        break;
        //    case Direction.Up:
        //        if(up)
        //            transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1 * 5));
        //        break;
        //    case Direction.Down:
        //        if(down)
        //            transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * -1 * 5));
        //        break;
        //}

        var curRotation = transform.localEulerAngles.z;

        blocked = IsfacingObstacle() || IsFacingEnemy();

        if (!blocked)
        {
            switch (curDireciton)
            {
                case Direction.Left:
                    if (left && curRotation == 90)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * Constant.TANK_WEIGHT * horizontal));
                    break;
                case Direction.Right:
                    if (right && curRotation == 270)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * Constant.TANK_WEIGHT * horizontal));
                    break;
                case Direction.Up:
                    if (up && (curRotation <= 0.001 && curRotation >= -0.001))
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * Constant.TANK_WEIGHT * vertical));
                    break;
                case Direction.Down:
                    if (down && curRotation == 180)
                        transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * Constant.TANK_WEIGHT * vertical));
                    break;
            }
        }
        
    }

    private void HandleFire()
    {
        if (Input.GetKey(fireKey))
        {
            if (fireTimer >= fireRate)
            {
                Fire(true);
            }
        }
    }

    bool IsFacingEnemy()
    {
        var curRotation = transform.localEulerAngles.z;

        if (curRotation == 90 || curRotation == 270)
            return Physics2D.OverlapBox(wallDetection.position, new Vector2(0.5f, 2.2f), 0, LayerMask.NameToLayer("Tank"));
        else
            return Physics2D.OverlapBox(wallDetection.position, new Vector2(2.2f, 0.5f), 0, LayerMask.NameToLayer("Tank"));
    }

    void PrintDebug()
    {
        string log = "";

        inputs.ToList().ForEach(x => log += x.ToString());

        Debug.Log(log);
    }
}
