using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryScreenController : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject itemList;

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private bool isInventoryOpen;

    [Header("Events")]
    [SerializeField] private SOEvent itemUse;
    [SerializeField] private SOEvent itemEquip;
    [SerializeField] private SOEvent itemUnequip;

    public void OnSetPlayerObject(SOEventArgs e)
    {
        var arg = (SOEventArgOne<Actor>)e;
        player = arg.arg;
    }

    public void OnInventoryOpen()
    {
        // if(isInventoryOpen)
        // {
        //     return;
        // }

        // //TODO: Пересмотреть хранение предметов в Actor. Возможно, сделать инвентарь отдельной сущностью
        // foreach(var item in player.inventory.items)
        // {
        //     var obj = Instantiate(itemPrefab, itemList.transform);
        //     //Set obj properties here
        // }

        // inventoryScreen.SetActive(true);
        isInventoryOpen = true;
    }

    public void OnInventoryClose()
    {
        inventoryScreen.SetActive(false);

        foreach (Transform item in itemList.transform)
        {
            Destroy(item.gameObject);
        }

        isInventoryOpen = false;
    }
}
