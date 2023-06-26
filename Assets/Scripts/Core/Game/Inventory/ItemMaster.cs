using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    private Item choosedItem;

    public void OnSetPlayerObject(SOEventArgs e)
    {
        var arg = (SOEventArgOne<Actor>)e;
        player = arg.arg;
    }

    public void OnItemChoosed(SOEventArgs e)
    {
        var obj = (SOEventArgOne<ItemUIWrapper>)e;
        choosedItem = obj.arg.GetItem();

        if (choosedItem.type != ItemType.UsableOnSelf) return;

        if (!player.HasEnoughInitiative(choosedItem.costInInitiativePercent))
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("������������ ����������!"));
            Debug.Log("Not enough initiative!");
            return;
        }

        if (!choosedItem.CanUse())
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("���� ������� ������ ������ ������������"));
            Debug.Log("Cannot use this item anymore");
            return;
        }

        Debug.Log($"Player use item {choosedItem.name.GetValue()} on self");
        player.ReduceInitiativeOnCost(choosedItem.costInInitiativePercent);
        choosedItem.Use(player, player);
        SOEventKeeper.Instance.GetEvent("onPlayerUsedItemOnSelf").Raise(new SOEventArgOne<Item>(choosedItem));
        SOEventKeeper.Instance.GetEvent("onItemUsed").Raise(new SOEventArgOne<Item>(choosedItem));

        choosedItem = null;
    }

    public void PlayerHasChoseTarget(SOEventArgs e)
    {
        if (choosedItem == null) return;

        var obj = (SOEventArgOne<List<EnemyUIWrapper>>)e;

        if (!player.HasEnoughInitiative(choosedItem.costInInitiativePercent))
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("������������ ����������!"));
            Debug.Log("Not enough initiative!");
            return;
        }

        if (!choosedItem.CanUse())
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("���� ������� ������ ������ ������������"));
            Debug.Log("Cannot use this item anymore");
            return;
        }

        player.ReduceInitiativeOnCost(choosedItem.costInInitiativePercent);

        foreach (var enemy in obj.arg)
        {
            Debug.Log($"Player use item {choosedItem.name.GetValue()} on {enemy.GetActor().name.GetValue()}");
            choosedItem.Use(player, enemy.GetActor());
        }

        SOEventKeeper.Instance.GetEvent("onPlayerUsedItemOnEnemy").Raise(new SOEventArgTwo<List<EnemyUIWrapper>, Item>(obj.arg, choosedItem));
        SOEventKeeper.Instance.GetEvent("onItemUsed").Raise(new SOEventArgOne<Item>(choosedItem));

        choosedItem = null;
    }

    public void OnAttunementToAnItem(SOEventArgs e)
    {

    }

    public void onPlayerHasChoseSkill(SOEventArgs e)
    {
        choosedItem = null;
    }
}
