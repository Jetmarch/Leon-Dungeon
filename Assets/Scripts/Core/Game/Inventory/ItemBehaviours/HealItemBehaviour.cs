using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealItemBehaviour", menuName = "SOObjects/ItemBehaviours/Heal")]
public class HealItemBehaviour : ItemBehaviour
{
    public int healAmount;

    public override void Use(Actor user, Actor target)
    {
        Debug.Log("Heal item was used!");
        var effect = new Effect();
        effect.SetDamage(healAmount);
        target.Affect(effect);
    }

    public override void StartAffect(Actor target)
    {
        
    }

    public override void EndAffect(Actor target)
    {
        
    }
}
