using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBrain : ScriptableObject
{
    protected Actor playerTarget;
    protected Actor brainOwner;
    public abstract void MakeMove(Actor player, Actor brainOwner);
    public abstract void OnAttackAnimationEnd();
}
