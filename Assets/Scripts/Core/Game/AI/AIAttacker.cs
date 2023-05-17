using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AIBrain/AIAttacker", fileName = "AIAttacker")]
public class AIAttacker : AIBrain
{
    public override void MakeMove(Actor player, Actor brainOwner)
    {
        Debug.Log("Enemy attacks!");
        SOEventKeeper.Instance.GetEvent("startAttackAnimation").Raise();
    }

    public override void OnAttackAnimationEnd()
    {
        EndOfTurn();
    }

    private void EndOfTurn()
    {
        Debug.Log("Enemy turn end!");
        SOEventKeeper.Instance.GetEvent("actorTurnEnd").Raise();
    }
}
