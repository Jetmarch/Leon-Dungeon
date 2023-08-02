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

    public void OnPlayerReadyUseItemInBattle(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Item>)e;
        choosedItem = obj.arg;

        if (choosedItem.type != ItemType.UsableOnSelf) return;

        choosedItem.Use(player, player);
        SOEventKeeper.Instance.GetEvent("onPlayerUsedItemOnSelf").Raise(new SOEventArgOne<Item>(choosedItem));
        SOEventKeeper.Instance.GetEvent("onItemUsed").Raise(new SOEventArgOne<Item>(choosedItem));

        choosedItem = null;
    }

    public void PlayerHasChoseTarget(SOEventArgs e)
    {
        if (choosedItem == null) return;

        var obj = (SOEventArgOne<List<EnemyUIWrapper>>)e;

        foreach (var enemy in obj.arg)
        {
            Debug.Log($"Player use item {choosedItem.name.GetValue()} on {enemy.GetActor().name.GetValue()}");
            choosedItem.Use(player, enemy.GetActor());
        }

        SOEventKeeper.Instance.GetEvent("onPlayerUsedItemOnEnemy").Raise(new SOEventArgTwo<List<EnemyUIWrapper>, Item>(obj.arg, choosedItem));
        SOEventKeeper.Instance.GetEvent("onItemUsed").Raise(new SOEventArgOne<Item>(choosedItem));

        choosedItem = null;
    }

    public void OnItemChooseTravel(SOEventArgs e)
    {
        var obj = (SOEventArgOne<ItemUIWrapper>)e;
        Item item = obj.arg.GetItem();

        if (item.type != ItemType.UsableOnSelf) return;

        item.Use(player, player);
        SOEventKeeper.Instance.GetEvent("onPlayerUsedItemOnSelf").Raise(new SOEventArgOne<Item>(item));
        SOEventKeeper.Instance.GetEvent("onItemUsed").Raise(new SOEventArgOne<Item>(item));
    }

    public void OnAttunementToAnItem(SOEventArgs e)
    {

    }

    public void onPlayerHasChoseSkillBattle(SOEventArgs e)
    {
        choosedItem = null;
    }

    public void OnEscapeKeyDown()
    {
        choosedItem = null;
        SOEventKeeper.Instance.GetEvent("onItemAbandone").Raise();
    }
}
