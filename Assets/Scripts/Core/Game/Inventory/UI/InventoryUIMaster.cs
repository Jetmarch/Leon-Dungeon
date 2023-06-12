using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIMaster : MonoBehaviour
{
    [SerializeField] private InventoryMaster inventoryMaster;
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject itemList;

    [SerializeField] private GameObject itemUIWrapperPrefab;
    [SerializeField] private Button closeInventoryButton;

    [SerializeField] private bool isInventoryOpen;

    private void Awake()
    {
        closeInventoryButton.onClick.AddListener(OnInventoryClose);
    }

    public void OnInventoryOpen()
    {    
        if (isInventoryOpen == true) return;
        isInventoryOpen = true;

        
        inventoryScreen.SetActive(true);
        //TODO: open inventory with animation
        //SOEventKeeper.Instance.GetEvent("onInventoryOpenAnimationStart").Raise();

        CleanInventory();
        FillInventory();
    }

    public void OnInventoryOpenAnimationEnd()
    {
        CleanInventory();
        FillInventory();
    }

    public void OnInventoryClose()
    {
        CleanInventory();

        isInventoryOpen = false;
        inventoryScreen.SetActive(false);
    }

    public void OnItemChoose()
    {
        OnInventoryClose();
    }

    private void FillInventory()
    {
        var listOfItems = inventoryMaster.GetAllItems();

        foreach (var item in listOfItems)
        {
            var obj = Instantiate(itemUIWrapperPrefab, itemList.transform);
            obj.GetComponent<ItemUIWrapper>().SetItem(item);
        }
    }

    private void CleanInventory()
    {
        foreach (Transform item in itemList.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
