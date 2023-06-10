using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemHolder : MonoBehaviour
{
    [SerializeField] private Item item;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnUseItem);
    }

    public void OnUseItem()
    {
        Debug.Log($"Item {item.name} was choosed");
        SOEventKeeper.Instance.GetEvent("onItemChoose").Raise(new SOEventArgOne<Item>(item));
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
