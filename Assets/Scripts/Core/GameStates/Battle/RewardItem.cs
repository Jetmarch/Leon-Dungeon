using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    public void SetItem(Item item) 
    {
        this.item = item;
        itemIcon.sprite = item.icon;
        itemName.text = item.name.GetValue();
    }

    public Item GetItem()
    {
        return item;
    }
}
