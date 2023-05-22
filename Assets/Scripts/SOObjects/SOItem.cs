using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SOObjects/SOItem", fileName ="SOItem")]
public class SOItem : ScriptableObject
{
    public new LocaleString name;
    public LocaleString description;
    public ItemType type;
    public int countOfMaxUse;
    
    public List<ItemBehaviour> useBehaviours;
    public List<ItemBehaviour> eqiupBehaviours;
}
