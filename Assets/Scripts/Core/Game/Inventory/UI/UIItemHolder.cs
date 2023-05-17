using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemHolder : MonoBehaviour
{
    public Item item;

    [Header("Events")]
    [SerializeField] private SOEvent itemUse;
    [SerializeField] private SOEvent itemEquip;
    [SerializeField] private SOEvent itemUnequip;

    public void OnUseItem()
    {
        Debug.Log($"Item {item.name} was used");
        itemUse.Raise(new SOEventArgOne<Item>(item));
    }

    public void OnEquipItem()
    {
        Debug.Log($"Item {item.name} was equiped");
        itemEquip.Raise(new SOEventArgOne<Item>(item));
    }

    public void OnUnequipItem()
    {
        Debug.Log($"Item {item.name} was unequiped");
        itemUnequip.Raise(new SOEventArgOne<Item>(item));
    }
}
