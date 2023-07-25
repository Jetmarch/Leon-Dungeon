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
        currentDurationLeft = maxDurationInTurns;
        buffEffect.StartAffect(target, owner);
        SOEventKeeper.Instance.GetEvent("onBuffStartAffect").Raise(new SOEventArgTwo<Actor, Buff>(target, this));
    }

    public void UpdateAffect(Actor target)
    {
        currentDurationLeft--;

        buffEffect.UpdateAffect(target, owner);
        SOEventKeeper.Instance.GetEvent("onBuffUpdateAffect").Raise(new SOEventArgTwo<Actor, Buff>(target, this));

        if (currentDurationLeft <= 0)
        {
            EndAffect(target);
            return;
        }
    }

    public void EndAffect(Actor target)
    {
        buffEffect.EndAffect(target, owner);
        target.buffs.Remove(this);
        SOEventKeeper.Instance.GetEvent("onBuffEndAffect").Raise(new SOEventArgTwo<Actor, Buff>(target, this));
    }
}