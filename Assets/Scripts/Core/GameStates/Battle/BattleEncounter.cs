using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEncounter : MonoBehaviour
{
    [SerializeField] private List<SOActor> enemies;
    [SerializeField] private List<Loot> items;
    public void StartBattle()
    {
        var listOfItemsFromLoot = new List<Item>();
        foreach(var loot in items)
        {
            var newItem = loot.GetItemWithChanceOfDrop();
            if(newItem != null)
            {
                listOfItemsFromLoot.Add(newItem);
            }
        }

        SOEventKeeper.Instance.GetEvent("startBattle").Raise(new SOEventArgOne<Battle>(new Battle(enemies, listOfItemsFromLoot)));
    }

    private void Start() {
        StartBattle();
    }

}
