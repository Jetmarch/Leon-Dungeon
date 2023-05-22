using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Loot
{
    [Range(0f, 100f)]
    public float chanceOfDrop;
    public SOItem item;

    public Item GetItemWithChanceOfDrop()
    {
        if (UnityEngine.Random.Range(0f, 100f) <= chanceOfDrop)
        {
            return new Item(item);
        }

        return null;
    }

    public Item GetItem()
    {
        return new Item(item);
    }
}
