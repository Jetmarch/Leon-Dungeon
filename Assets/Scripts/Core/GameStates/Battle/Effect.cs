using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Effect
{
    private float damage;
    private float heal;
    private List<Buff> buffs;

    public Effect()
    {
        buffs = new List<Buff>();
    }

    public void SetDamage(float amount)
    {
        damage = amount;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetHeal(float amount)
    {
        heal = amount;
    }

    public float GetHeal()
    {
        return heal;
    }

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
    }

    public List<Buff> GetBuffs()
    {
        return buffs;
    }
}
