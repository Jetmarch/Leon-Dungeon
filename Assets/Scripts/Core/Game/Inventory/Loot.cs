using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Loot
{
    [Range(0f, 100f)]
    public float chanceOfDrop;
    public Item item;
}
