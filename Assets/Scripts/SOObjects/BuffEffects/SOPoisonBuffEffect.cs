using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffEffect", menuName = "SOObjects/BuffEffects/PoisonBuffEffect")]
public class SOPoisonBuffEffect : SOBuffEffect
{
    public float poisonMinDamage;
    public float poisonMaxDamage;

    public override void StartAffect(Actor target, Actor user)
    {
        Debug.Log($"{target.name.GetValue()} poisoned!");
    }
    public override void UpdateAffect(Actor target, Actor user)
    {
        float damage = -(Random.Range(poisonMinDamage, poisonMaxDamage));
        Debug.Log($"{target.name.GetValue()} poisoned on {damage} damage!");
        target.healthStatus.ChangeHealth(damage);
    }
     public override void EndAffect(Actor target, Actor user)
    {
        Debug.Log($"{target.name.GetValue()} not poisoned anymore!");
    }
}
