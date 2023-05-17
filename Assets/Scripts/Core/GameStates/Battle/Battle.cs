using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Battle
{ 
    public List<Actor> enemies = new List<Actor>();
    //Временное решение. Здесь должен быть объект Loot
    public List<SOItem> loot = new List<SOItem>();


    public Battle(List<SOActor> soEnemies, List<SOItem> loot)
    {
        foreach(var enemy in soEnemies)
        {
            enemies.Add(new Actor(enemy));
        }

        this.loot = loot;
    }
}
