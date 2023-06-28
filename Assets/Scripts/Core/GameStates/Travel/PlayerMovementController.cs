using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;

    private void Awake()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    public void OnMoveForwardKeyDown()
    {
        pm.OnMoveForward();
    }

    public void OnMoveBackwardKeyDown()
    {
        pm.OnMoveBackward();
    }

    public void OnMoveRightKeyDown()
    {
        pm.OnMoveRight();
    }

    public void OnMoveLeftKeyDown()
    {
        pm.OnMoveLeft();
    }

    public void OnTurnLeftKeyDown()
    {
        pm.Turn(90);
    }

    public void OnTurnRightKeyDown()
    {
        pm.Turn(-90);
    }
}
