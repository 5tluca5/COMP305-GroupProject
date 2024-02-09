using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using DG.Tweening;

public enum Direction : int
{
    Left = 0,
    Right,
    Up,
    Down,
    None
}

public class PlayerController : PlayerTank
{
    [Header("Controll Keys")]
    [SerializeField] KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] KeyCode rightKey = KeyCode.RightArrow;
    [SerializeField] KeyCode upKey = KeyCode.UpArrow;
    [SerializeField] KeyCode downKey = KeyCode.DownArrow;

    [SerializeField] Direction curDireciton = Direction.None;
    Stack<Direction> inputs = new Stack<Direction>();

    private void LateUpdate()
    {
        HandleMovement();

    }

    private void HandleMovement()
    {
        var left = Input.GetKey(leftKey);
        var right = Input.GetKey(rightKey);
        var up = Input.GetKey(upKey);
        var down = Input.GetKey(downKey);

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
        switch (curDireciton)
        {
            case Direction.Left:
                if (left && transform.localEulerAngles.z == 90)
                    transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1 * 1));
                break;
            case Direction.Right:
                if (right && transform.localEulerAngles.z == 270)
                    transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1 * 1));
                break;
            case Direction.Up:
                if (up && (transform.localEulerAngles.z <= 0.001 && transform.localEulerAngles.z >= -0.001))
                    transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1 * 1));
                break;
            case Direction.Down:
                if (down && transform.localEulerAngles.z == 180)
                    transform.Translate(new Vector2(0, stat.movementSpeed * Time.deltaTime * 1 * 1));
                break;
        }
    }

    void DoRotation(Direction direction)
    {
        //Debug.Log("Transform rotation: " + transform.localRotation + " Transform localEulerAngles: " + transform.localEulerAngles);
        var curRotation = transform.localEulerAngles.z;

        switch (direction)
        {
            case Direction.Left:
                transform.DORotate(new Vector3(0, 0, 90), curRotation == 270 ? 0.4f : 0.2f);
                break;
            case Direction.Right:
                transform.DORotate(new Vector3(0, 0, -90), curRotation == 90 ? 0.4f : 0.2f);
                break;
            case Direction.Up:
                transform.DORotate(new Vector3(0, 0, 0), curRotation == 180 ? 0.4f : 0.2f);
                break;
            case Direction.Down:
                transform.DORotate(new Vector3(0, 0, -180), curRotation == 0 ? 0.4f : 0.2f);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintDebug()
    {
        string log = "";

        inputs.ToList().ForEach(x => log += x.ToString());

        Debug.Log(log);
    }
}
