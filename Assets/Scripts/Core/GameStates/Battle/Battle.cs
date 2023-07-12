using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Battle
{ 
    public List<Actor> enemies = new List<Actor>();
    //Временное решение. Здесь должен быть объект Loot
    public List<Item> loot = new List<Item>();


    public Battle(List<SOActor> soEnemies, List<Item> _loot)
    {
        foreach(var enemy in soEnemies)
        {
            enemies.Add(new Actor(enemy));
        }

        this.loot = _loot;
    }

    public Battle(List<SOActor> soEnemies, List<Loot> _loot)
    {
        foreach(var item in _loot)
        {
            Item newItem = item.GetItemWithChanceOfDrop();
            if (newItem != null)
            {
                loot.Add(newItem);
            }
        }

        foreach (var enemy in soEnemies)
        {
            enemies.Add(new Actor(enemy));
        }
    }
}
