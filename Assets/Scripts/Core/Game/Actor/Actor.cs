using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Actor
{
    public LocaleString name;
    public Sprite sprite;
    public HealthStatus healthStatus;
    public ActorStats stats;
    public AIBrain brain;

    public float Initiative;
    public Skill baseAttack;
    public Skill baseDefend;
    public Skill basePassTurn;
    public List<Skill> additionalSkills;
    public List<Buff> buffs;

    public Actor(SOActor _actor)
    {
        this.name = _actor.name;
        this.sprite = _actor.sprite;
        this.stats = _actor.stats;
        this.brain = _actor.brain;
        this.baseAttack = _actor.baseAttack;
        this.baseDefend = _actor.baseDefend;
        this.basePassTurn = _actor.basePassTurn;
        this.additionalSkills = _actor.additionalSkills;

        if(brain != null) this.brain.SetOwner(this);

        this.healthStatus = new HealthStatus(this);
        this.buffs = new List<Buff>();
    }

    public void InitiativeStep(float timeDelta)
    {
        float initiativeInSec = (stats.speed + (stats.agility / 10f)) * timeDelta;
        Initiative = Mathf.Clamp(initiativeInSec + Initiative, 0f, 1f);

        SOEventKeeper.Instance.GetEvent("onInitiativeChanged").Raise(new SOEventArgOne<Actor>(this));
    }

    public void InitiativeReset()
    {
        Initiative = 0;
    }

    public void ChangeInitiative(float amount)
    {
        Initiative = Mathf.Clamp(amount + Initiative, 0f, 1f);

        SOEventKeeper.Instance.GetEvent("onInitiativeChanged").Raise(new SOEventArgOne<Actor>(this));
    }

    public bool HasEnoughInitiative(Skill skill)
    {
        float amount = skill.costInInitiativePercent / 100;
        if(Initiative >= amount)
        {
            return true;
        }

        return false;
    }

    public void ReduceInitiativeOnSkillCost(Skill skill)
    {
        float amount = -(skill.costInInitiativePercent / 100);
        ChangeInitiative(amount);
    }

    public bool IsReady()
    {
        if(Initiative >= 1f)
        {
            return true;
        }

        return false;
    }

    public void Affect(Effect effect)
    {
        //TODO damaging, buffing, debuffing
        healthStatus.ChangeHealth(effect.GetDamage());

        foreach(var buff in effect.GetBuffs())
        {
            buffs.Add(buff);
            buff.StartAffect(this);
        }
    }

    public bool HasBuff(Buff buff)
    {
        foreach(var b in buffs)
        {
            if(b.name == buff.name)
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateBuffs()
    {
        foreach(var buff in buffs)
        {
            buff.UpdateAffect(this);
        }
    }
}
