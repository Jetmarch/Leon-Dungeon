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

    [Range(0, 100)]
    public int costInInitiativePercent;
    
    public List<ItemBehaviour> useBehaviours;
    public List<ItemBehaviour> eqiupBehaviours;
    public List<ItemBehaviour> attunementBehaviours;

    public Item(SOItem _item)
    {
        this.name = _item.name;
        this.icon = _item.icon;
        this.description = _item.description;
        this.type = _item.type;
        this.countOfMaxUse = _item.countOfMaxUse;
        this.costInInitiativePercent = _item.costInInitiativePercent;
        this.useBehaviours = _item.useBehaviours;
        this.eqiupBehaviours = _item.eqiupBehaviours;
    }

    public void Use(Actor user, Actor target)
    {
        foreach(var behaviour in useBehaviours)
        {
            behaviour.Use(user, target);
        }
    }

    public void Equip(Actor user)
    {
        foreach(var behaviour in eqiupBehaviours)
        {
            behaviour.StartAffect(user);
        }
    }

    public void Unequip(Actor user)
    {
        foreach(var behaviour in eqiupBehaviours)
        {
            behaviour.EndAffect(user);
        }
    }

    public void Attunement(Actor user)
    {
        //TODO: attunement logic here
    }
}
