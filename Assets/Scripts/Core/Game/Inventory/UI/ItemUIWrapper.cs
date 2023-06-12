using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIWrapper : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnUseItem);
    }

    public void OnUseItem()
    {
        Debug.Log($"Item {item.name.GetValue()} was choosed");
        SOEventKeeper.Instance.GetEvent("onItemChoose").Raise(new SOEventArgOne<ItemUIWrapper>(this));
    }

    public void OnEquipItem()
    {
        Debug.Log($"Item {item.name.GetValue()} was equiped");
        SOEventKeeper.Instance.GetEvent("onItemEquip").Raise(new SOEventArgOne<Item>(item));
    }

    public void OnUnequipItem()
    {
        Debug.Log($"Item {item.name.GetValue()} was unequiped");
        SOEventKeeper.Instance.GetEvent("onItemUnequip").Raise(new SOEventArgOne<Item>(item));
    }
    
    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        itemIcon.sprite = item.icon;
        itemName.text = item.name.GetValue();
    }
}
