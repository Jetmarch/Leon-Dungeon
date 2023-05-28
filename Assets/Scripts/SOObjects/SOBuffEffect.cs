using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOBuffEffect : ScriptableObject
{
    public abstract void StartAffect(Actor target, Actor user);

    public abstract void UpdateAffect(Actor target, Actor user);

    public abstract void EndAffect(Actor target, Actor user);
}
