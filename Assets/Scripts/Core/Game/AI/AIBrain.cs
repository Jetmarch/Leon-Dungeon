using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBrain : ScriptableObject
{
    protected Actor playerTarget;
    protected Actor brainOwner;
    public abstract List<Skill> MakeMoveOnFullInitiativeAndGetListOfSkills();

    public void SetOwner(Actor actor)
    {
        brainOwner = actor;
    }
}
