using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "SOObjects/SkillEffects/Debuff")]
public class DebuffSkillEffect : SkillEffect
{
    public List<SOBuff> buffs;
    public override void Affect(Actor target, Actor user)
    {
        var effect = new Effect();
        
        foreach(var buff in buffs)
        {
            effect.AddBuff(new Buff(buff, user));
        }

        
        Debug.Log($"User {user.name.GetValue()} debuffed target {target.name.GetValue()} on {effect.GetDamage()} damage");
        target.Affect(effect);
    }
}
