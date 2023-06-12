using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private Item choosedItem;

    public void OnSetPlayerObject(SOEventArgs e)
    {
        var arg = (SOEventArgOne<Actor>)e;
        player = arg.arg;
    }

    public void OnItemChoosed(SOEventArgs e)
    {
        //If ItemType.UsableOnSelf - use
        //else choosedItem = item
        var obj = (SOEventArgOne<ItemUIWrapper>)e;
        choosedItem = obj.arg.GetItem();

        if (choosedItem.type != ItemType.UsableOnSelf) return;

        if (!player.HasEnoughInitiative(choosedItem.costInInitiativePercent))
        {
            Debug.Log("Not enough initiative!");
            return;
        }

        Debug.Log($"Player use item {choosedItem.name.GetValue()} on self");
        player.ReduceInitiativeOnCost(choosedItem.costInInitiativePercent);
        choosedItem.Use(player, player);
        SOEventKeeper.Instance.GetEvent("onPlayerUsedItemOnSelf").Raise(new SOEventArgOne<Item>(choosedItem));
    }

    public void OnItemTargetChoosed(SOEventArgs e)
    {
        
    }

    public void OnAttunementToAnItem(SOEventArgs e)
    {

    }
}
