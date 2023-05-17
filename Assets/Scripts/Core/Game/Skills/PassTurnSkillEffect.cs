using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "SOObjects/SkillEffects/PassTurn")]
public class PassTurnSkillEffect : SkillEffect
{
    public override void Affect(Actor target, Actor user)
    {
        Debug.Log($"Actor {user.name.GetValue()} pass turn");
        user.InitiativeReset();
        SOEventKeeper.Instance.GetEvent("actorTurnEnd").Raise(new SOEventArgOne<Actor>(user));
    }
}
