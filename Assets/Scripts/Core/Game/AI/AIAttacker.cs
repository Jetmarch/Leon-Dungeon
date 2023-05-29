using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AIBrain/AIAttacker", fileName = "AIAttacker")]
public class AIAttacker : AIBrain
{
    public override List<Skill> MakeMoveOnFullInitiativeAndGetListOfSkills()
    {
        //Для теста обычный атакующий будет использовать каждый ход базовую атаку
        var listOfSkills = new List<Skill>();
        listOfSkills.Add(brainOwner.baseAttack);
        listOfSkills.Add(brainOwner.baseAttack);
        listOfSkills.Add(brainOwner.baseAttack);

        return listOfSkills;
    }
}
