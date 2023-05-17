using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public LocaleString name;
    public LocaleString description;
    public ItemType type;
    public int countOfMaxUse;
    
    public List<ItemBehaviour> useBehaviours;
    public List<ItemBehaviour> eqiupBehaviours;
    public Item(SOItem _item)
    {
        this.name = _item.name;
        this.description = _item.description;
        this.type = _item.type;
        this.countOfMaxUse = _item.countOfMaxUse;
        this.useBehaviours = _item.useBehaviours;
        this.eqiupBehaviours = _item.eqiupBehaviours;
    }
}
