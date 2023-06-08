using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public LocaleString name;
    public Sprite icon;
    public LocaleString description;
    public ItemType type;
    public int countOfMaxUse;
    
    public List<ItemBehaviour> useBehaviours;
    public List<ItemBehaviour> eqiupBehaviours;

    public Item(SOItem _item)
    {
        this.name = _item.name;
        this.icon = _item.icon;
        this.description = _item.description;
        this.type = _item.type;
        this.countOfMaxUse = _item.countOfMaxUse;
        this.useBehaviours = _item.useBehaviours;
        this.eqiupBehaviours = _item.eqiupBehaviours;
    }

    public void OnUse(Actor user, Actor target)
    {
        foreach(var behaviour in useBehaviours)
        {
            behaviour.Use(user, target);
        }
    }

    public void OnEquip(Actor user)
    {
        foreach(var behaviour in eqiupBehaviours)
        {
            behaviour.StartAffect(user);
        }
    }

    public void OnUnequip(Actor user)
    {
        foreach(var behaviour in eqiupBehaviours)
        {
            behaviour.EndAffect(user);
        }
    }
}
