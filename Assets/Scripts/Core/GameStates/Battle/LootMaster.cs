using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private Battle currentBattle;
    [SerializeField] private GameObject lootScreenParent;
    [SerializeField] private GameObject lootList;
    [SerializeField] private GameObject lootItemPrefab;

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }


    public void OnStartBattle(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Battle>)e;
        currentBattle = obj.arg;
    }

    public void OnVictoryInBattle(SOEventArgs e)
    {
        lootScreenParent.SetActive(true);
        //Start openning animation
        //Start animation of adding items in list or smt
    }

    public void OnLootScreenClose()
    {
        //Start closing animation
        lootScreenParent.SetActive(false);
    }

    private void AddLootInList()
    {
        foreach(var item in currentBattle.loot)
        {
            var obj = Instantiate(lootItemPrefab, lootList.transform);
            //Get LootWrapper
            //Set object parameters
        }
    }
}
