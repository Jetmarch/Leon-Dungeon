using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMaster : MonoBehaviour
{
    [SerializeField] private List<Item> inventory;

    public void OnAddItem(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Item>)e;
        inventory.Add(obj.arg);

        SOEventKeeper.Instance.GetEvent("onAddItemEnd").Raise();
    }

    public void OnAddItems(SOEventArgs e)
    {
        var obj = (SOEventArgOne<List<Item>>)e;
        inventory.AddRange(obj.arg);

        SOEventKeeper.Instance.GetEvent("onAddItemEnd").Raise();
    }

    public void OnRemoveItem(SOEventArgs e)
    {

    }

    public void OnItemUsed(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Item>)e;
        if(!obj.arg.CanUse())
        {
            inventory.Remove(obj.arg);
            SOEventKeeper.Instance.GetEvent("onRemoveItem").Raise(new SOEventArgOne<Item>(obj.arg));
        }
    }

    public void OnEquipItem(SOEventArgs e)
    {

    }

    public void OnUnequipItem(SOEventArgs e)
    {

    }

    public void OnAttunementToAnItem(SOEventArgs e)
    {

    }

    public List<Item> GetAllItems()
    {
        return inventory;
    }
}
