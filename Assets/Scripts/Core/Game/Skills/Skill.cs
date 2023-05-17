using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skill
{
    public LocaleString name;
    public LocaleString description;

    public Sprite icon;
    public SkillType type;

    [Range(0, 100)]
    public float costInInitiativePercent;

    public List<SkillEffect> effects;
    public void Use(List<Actor> targets, Actor user)
    {
        foreach(Actor actor in targets)
        {
            foreach (SkillEffect effect in effects)
            {
                effect.Affect(actor, user);
            }
        }
    }

    public void Use(Actor target, Actor user)
    {
        foreach(SkillEffect effect in effects)
        {
            effect.Affect(target, user);
        }
    }
}

public enum SkillType
{
    SingleTarget,
    MassTarget,
    SelfTarget
}