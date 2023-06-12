using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Buff
{
    public LocaleString name;
    public LocaleString description;
    public int maxDurationInTurns;
    public int currentDurationLeft;
    public BuffType type;
    public Sprite icon;
    public SOBuffEffect buffEffect;
    public Actor owner;

    public Buff(SOBuff _buff, Actor _owner)
    {
        name = _buff.name;
        description = _buff.description;
        maxDurationInTurns = _buff.maxDurationInTurns;
        currentDurationLeft = maxDurationInTurns;
        type = _buff.type;
        icon = _buff.icon;
        buffEffect = _buff.buffEffect;
        owner = _owner;
    }

    public void StartAffect(Actor target)
    {
        //Если уже есть такой бафф, то не накладываем повторно
        if(target.HasBuff(this)) return;

        Debug.Log($"Buff {name.GetValue()} start affecting {target.name.GetValue()}");
        currentDurationLeft = maxDurationInTurns;
        buffEffect.StartAffect(target, owner);
    }

    public void UpdateAffect(Actor target)
    {
        Debug.Log($"Buff {name.GetValue()} CurrentDurationLeft {currentDurationLeft}");
        currentDurationLeft--;

        Debug.Log($"Buff {name.GetValue()} continue affecting {target.name.GetValue()}");
        buffEffect.UpdateAffect(target, owner);
        if(currentDurationLeft <= 0)
        {
            EndAffect(target);
            return;
        }
    }

    public void EndAffect(Actor target)
    {
        Debug.Log($"Buff {name.GetValue()} end affecting {target.name.GetValue()}");
        buffEffect.EndAffect(target, owner);
        target.buffs.Remove(this);
    }
}