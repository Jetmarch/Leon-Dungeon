using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private KeyCode moveForward;
    [SerializeField] private KeyCode moveBackward;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode menu;
    [SerializeField] private KeyCode openInventory;
    [SerializeField] private KeyCode openSkillList;
    [SerializeField] private KeyCode escape;
    [SerializeField] private KeyCode space;
    
    
    void Update()
    {
        ProcessKey();
        ProcessKeyUp();
        ProcessKeyDown();
    }

    private void ProcessKeyDown()
    {
        if(Input.GetKeyDown(moveForward))
        {
            SOEventKeeper.Instance.GetEvent("onMoveForwardKeyDown").Raise();
        }
        if(Input.GetKeyDown(moveBackward))
        {
            SOEventKeeper.Instance.GetEvent("onMoveBackwardKeyDown").Raise();
        }
        if(Input.GetKeyDown(moveRight))
        {
            SOEventKeeper.Instance.GetEvent("onMoveRightKeyDown").Raise();
        }
        if(Input.GetKeyDown(moveLeft))
        {
            SOEventKeeper.Instance.GetEvent("onMoveLeftKeyDown").Raise();
        }
        if(Input.GetKeyDown(turnLeft))
        {
            SOEventKeeper.Instance.GetEvent("onTurnLeftKeyDown").Raise();
        }
        if(Input.GetKeyDown(turnRight))
        {
            SOEventKeeper.Instance.GetEvent("onTurnRightKeyDown").Raise();
        }
        if(Input.GetKeyDown(menu))
        {
            SOEventKeeper.Instance.GetEvent("onMenuKeyDown").Raise();
        }
        if(Input.GetKeyDown(openInventory))
        {
            SOEventKeeper.Instance.GetEvent("onOpenInventoryKeyDown").Raise();
        }
        if(Input.GetKeyDown(openSkillList))
        {
            SOEventKeeper.Instance.GetEvent("onOpenSkillListKeyDown").Raise();
        }
        if(Input.GetKeyDown(escape))
        {
            SOEventKeeper.Instance.GetEvent("onEscapeKeyDown").Raise();
        }
        if (Input.GetKeyDown(space))
        {
            SOEventKeeper.Instance.GetEvent("onSpaceKeyDown").Raise();
        }
    }

    private void ProcessKeyUp()
    {
        if(Input.GetKeyUp(moveForward))
        {
            SOEventKeeper.Instance.GetEvent("onMoveForwardKeyUp").Raise();
        }
        if(Input.GetKeyUp(moveBackward))
        {
            SOEventKeeper.Instance.GetEvent("onMoveBackwardKeyUp").Raise();
        }
        if(Input.GetKeyUp(moveRight))
        {
            SOEventKeeper.Instance.GetEvent("onMoveRightKeyUp").Raise();
        }
        if(Input.GetKeyUp(moveLeft))
        {
            SOEventKeeper.Instance.GetEvent("onMoveLeftKeyUp").Raise();
        }
        if(Input.GetKeyUp(turnLeft))
        {
            SOEventKeeper.Instance.GetEvent("onTurnLeftKeyUp").Raise();
        }
        if(Input.GetKeyUp(turnRight))
        {
            SOEventKeeper.Instance.GetEvent("onTurnRightKeyUp").Raise();
        }
        if(Input.GetKeyUp(menu))
        {
            SOEventKeeper.Instance.GetEvent("onMenuKeyUp").Raise();
        }
        if(Input.GetKeyUp(openInventory))
        {
            SOEventKeeper.Instance.GetEvent("onOpenInventoryKeyUp").Raise();
        }
        if(Input.GetKeyUp(openSkillList))
        {
            SOEventKeeper.Instance.GetEvent("onOpenSkillListKeyUp").Raise();
        }
        if (Input.GetKeyUp(escape))
        {
            SOEventKeeper.Instance.GetEvent("onEscapeKeyUp").Raise();
        }
        if (Input.GetKeyUp(space))
        {
            SOEventKeeper.Instance.GetEvent("onSpaceKeyUp").Raise();
        }
    }

    private void ProcessKey()
    {
        if(Input.GetKey(moveForward))
        {
            SOEventKeeper.Instance.GetEvent("onMoveForwardKey").Raise();
        }
        if(Input.GetKey(moveBackward))
        {
            SOEventKeeper.Instance.GetEvent("onMoveBackwardKey").Raise();
        }
        if(Input.GetKey(moveRight))
        {
            SOEventKeeper.Instance.GetEvent("onMoveRightKey").Raise();
        }
        if(Input.GetKey(moveLeft))
        {
            SOEventKeeper.Instance.GetEvent("onMoveLeftKey").Raise();
        }
        if(Input.GetKey(turnLeft))
        {
            SOEventKeeper.Instance.GetEvent("onTurnLeftKey").Raise();
        }
        if(Input.GetKey(turnRight))
        {
            SOEventKeeper.Instance.GetEvent("onTurnRightKey").Raise();
        }
        if(Input.GetKey(menu))
        {
            SOEventKeeper.Instance.GetEvent("onMenuKey").Raise();
        }
        if(Input.GetKey(openInventory))
        {
            SOEventKeeper.Instance.GetEvent("onOpenInventoryKey").Raise();
        }
        if(Input.GetKey(openSkillList))
        {
            SOEventKeeper.Instance.GetEvent("onOpenSkillListKey").Raise();
        }
        if (Input.GetKey(escape))
        {
            SOEventKeeper.Instance.GetEvent("onEscapeKey").Raise();
        }
        if (Input.GetKey(space))
        {
            SOEventKeeper.Instance.GetEvent("onSpaceKey").Raise();
        }
    }
}
