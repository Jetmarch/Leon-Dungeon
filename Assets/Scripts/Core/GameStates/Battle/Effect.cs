using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Effect
{
    private float damage;
    //private List<Buff> buffs;

    public void SetDamage(float amount)
    {
        damage = amount;
    }

    public float GetDamage()
    {
        return damage;
    }
}
