using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "SOObjects/SkillEffects/Heal")]
public class HealSkillEffect : SkillEffect
{
    public int amountMin;
    public int amountMax;
    public override void Affect(Actor target, Actor user)
    {
        var effect = new Effect();
        float damage = user.stats.Sturdiness * Random.Range(amountMin, amountMax);
        effect.SetHeal(damage);


        Debug.Log($"User {user.name.GetValue()} affected target {target.name.GetValue()} on {effect.GetDamage()} heal");
        target.Affect(effect);
    }
}
