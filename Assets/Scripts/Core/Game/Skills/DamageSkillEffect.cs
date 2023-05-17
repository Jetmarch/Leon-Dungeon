using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "SOObjects/SkillEffects/Damage")]
public class DamageSkillEffect : SkillEffect
{
    public int amountMin;
    public int amountMax;
    public override void Affect(Actor target, Actor user)
    {
        var effect = new Effect();
        float damage = -(user.stats.strength * Random.Range(amountMin, amountMax));
        effect.SetDamage(damage);

        
        Debug.Log($"User {user.name.GetValue()} affected target {target.name.GetValue()} on {effect.GetDamage()} damage");
        target.Affect(effect);
    }
}
