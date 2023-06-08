using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBehaviour : ScriptableObject
{
    //On single use
    public abstract void Use(Actor user, Actor target);

    //On equip item
    public abstract void StartAffect(Actor target);

    //On unequip
    public abstract void EndAffect(Actor target);
}
