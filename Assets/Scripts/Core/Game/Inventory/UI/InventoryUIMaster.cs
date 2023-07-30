using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIMaster : MonoBehaviour
{
    [SerializeField] protected InventoryMaster inventoryMaster;
    [SerializeField] protected GameObject inventoryScreen;
    [SerializeField] protected GameObject itemList;

    [SerializeField] protected GameObject itemUIWrapperPrefab;
    [SerializeField] protected Button closeInventoryButton;

    [SerializeField] protected bool isInventoryOpen;

    private void Awake()
    {
        closeInventoryButton.onClick.AddListener(OnCloseInventory);
    }

    public void OnOpenInventory()
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

    public void OnCloseInventory()
    {
        CleanInventory();

        isInventoryOpen = false;
        inventoryScreen.SetActive(false);
    }

    public void OnItemChoose()
    {
        OnCloseInventory();
    }

    public void OnRemoveItem(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Item>)e;

        var itemsWrapper = FindObjectsOfType<ItemUIWrapper>();
        for(int i = 0; i < itemsWrapper.Length; i++)
        {
            if(itemsWrapper[i].GetItem() == obj.arg)
            {
                Destroy(itemsWrapper[i].gameObject);
                return;
            }
        }
    }

    public void OnInventoryToggle()
    {
        if(isInventoryOpen)
        {
            OnCloseInventory();
        }
        else
        {
            OnOpenInventory();
        }
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
