using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    [SerializeField] private Item choosedItem;

    public void OnItemChoosed(SOEventArgs e)
    {
        //If ItemType.UsableOnSelf - use
        //else choosedItem = item
    }

    public void OnItemTargetChoosed(SOEventArgs e)
    {
        
    }
}
