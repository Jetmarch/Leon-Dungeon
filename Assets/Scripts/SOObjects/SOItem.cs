using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SOObjects/SOItem", fileName ="SOItem")]
public class SOItem : ScriptableObject
{
    public new LocaleString name;
    public Sprite icon;
    public LocaleString description;
    public ItemType type;
    public int countOfMaxUse;

    [Range(0, 100)]
    public int costInInitiativePercent;
    
    public List<ItemBehaviour> useBehaviours;
    public List<ItemBehaviour> eqiupBehaviours;
}
