using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 *  A simple dungeon crawler player movement script
 *  Written by zooperdan
 *  
 *  1) Create a layer named "Blocking" (or any other name you choose)
 *  2) Add this script to the Player gameobject.
 *  3) Set "Layer Mask" in inspector to the "Blocking" layer.
 *  4) Add Wall objects (Quads or Cubes) with colliders and place them on grid positions relative to PlayerMovement.stepSize
 *  5) Add all walls to the "Blocking" layer.
 *  
 *  Note: This script use iTween by Bob Berkebile of pixelplacement.com and it is free to use.
 *  http://www.pixelplacement.com/itween/index.php
 *  https://assetstore.unity.com/packages/tools/animation/itween-84
 *  
 */
 
public class PlayerMovement : MonoBehaviour
{

    public LayerMask layerMask;

    public bool continuedWalk = true;

    public float walkSpeed = 1.0f;
    public float walkToWallSpeed = 0.3f;
    public float turnSpeed = 1.0f;
    public float stepSize = 3.0f;

    public iTween.EaseType easeType;

    public KeyCode keyMoveForward = KeyCode.UpArrow;
    public KeyCode keyMoveBackward = KeyCode.DownArrow;
    public KeyCode keyStrafeLeft = KeyCode.Delete;
    public KeyCode keyStrafeRight = KeyCode.PageDown;
    public KeyCode keyTurnLeft = KeyCode.LeftArrow;
    public KeyCode keyTurnRight = KeyCode.RightArrow;

    private bool _isMoving = false;
    private bool _isTurning = false;

    public void OnMoveForward()
    {
        if (!_isMoving && !_isTurning)
        {
            Move(transform.forward * stepSize);
        }
    }

    public void OnMoveBackward()
    {
        if (!_isMoving && !_isTurning)
        {
            Move(-transform.forward * stepSize);
        }
    }

    public void OnMoveRight()
    {
        if (!_isTurning)
        {
            Turn(90f);
        }
    }

    public void OnMoveLeft()
    {
        if (!_isTurning)
        {
            Turn(-90f);
        }
    }

    private bool KeyDown(KeyCode key)
    {

        return continuedWalk && !_isMoving ? Input.GetKey(key) : Input.GetKeyDown(key);

    }

    private bool IsBlocked(Vector3 direction)
    {

        if (Physics.Raycast(
            transform.position + new Vector3(0f, 0.5f, 0f),
            direction + new Vector3(0f, 0.5f, 0f),
            out RaycastHit hit,
            stepSize, layerMask
        ))
        {
            MoveInWall(direction / 3);
            return true;
        }
        else
        {
            return false;
        }

    }

    public void OnMoved()
    {
        _isMoving = false;
        transform.position = new Vector3(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z)
        );
    }

    private void OnMovedInWall(Vector3 direction)
    {
        Hashtable ht = new Hashtable
                {
                    { "amount", -direction },
                    { "time", walkToWallSpeed },
                    { "space", Space.World },
                    { "oncomplete", "OnMoved" },
                    { "easeType", easeType },
                    { "looptype", iTween.LoopType.none }
                };
        iTween.MoveBy(this.gameObject, ht);
    }

    public void OnTurned()
    {
        _isTurning = false;
        var vec = transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        transform.eulerAngles = vec;
    }

    private void Move(Vector3 direction)
    {
        if (!IsBlocked(direction) && !_isMoving)
        {
            _isMoving = true;
            Hashtable ht = new Hashtable
                {
                    { "amount", direction },
                    { "time", walkSpeed },
                    { "space", Space.World },
                    { "oncomplete", "OnMoved" },
                    { "easeType", easeType },
                    { "looptype", iTween.LoopType.none }
                };
            iTween.MoveBy(this.gameObject, ht);
        }
    }

    private void MoveInWall(Vector3 direction)
    {
        if (_isMoving)
        {
            return;
        }

        _isMoving = true;
        Hashtable ht = new Hashtable
                {
                    { "amount", direction },
                    { "time", walkToWallSpeed },
                    { "space", Space.World },
                    { "oncomplete", "OnMovedInWall" },
                    { "oncompleteparams", direction },
                    { "easeType", easeType },
                    { "looptype", iTween.LoopType.none }
        };
        iTween.MoveBy(this.gameObject, ht);
    }

    private void Turn(float angle)
    {

        Vector3 rotationAmount = new Vector3(0, angle / 360, 0);

        if (!_isTurning)
        {
            _isTurning = true;
            Hashtable ht = new Hashtable
                {
                    { "amount", rotationAmount },
                    { "time", turnSpeed },
                    { "oncomplete", "OnTurned" },
                    { "easeType", easeType },
                    { "looptype", iTween.LoopType.none }
                };
            iTween.RotateBy(this.gameObject, ht);
        }

    }

}