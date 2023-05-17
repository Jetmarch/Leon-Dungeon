using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject
{
    public abstract void Affect(Actor target, Actor user);
}

