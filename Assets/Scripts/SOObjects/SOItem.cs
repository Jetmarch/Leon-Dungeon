using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOItem : ScriptableObject
{
    public new LocaleString name;
    public LocaleString description;
    public ItemType type;
    public int countOfMaxUse;
    
    public List<ItemBehaviour> useBehaviours;
    public List<ItemBehaviour> eqiupBehaviours;
}
