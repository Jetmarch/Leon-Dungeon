using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemHolder : MonoBehaviour
{
    public Item item;

    public void OnUseItem()
    {
        Debug.Log($"Item {item.name} was used");
        SOEventKeeper.Instance.GetEvent("onItemUse").Raise(new SOEventArgOne<Item>(item));
    }

    public void OnEquipItem()
    {
        Debug.Log($"Item {item.name} was equiped");
        SOEventKeeper.Instance.GetEvent("onItemEquip").Raise(new SOEventArgOne<Item>(item));
    }

    public void OnUnequipItem()
    {
        Debug.Log($"Item {item.name} was unequiped");
        SOEventKeeper.Instance.GetEvent("onItemUnequip").Raise(new SOEventArgOne<Item>(item));
    }
}
