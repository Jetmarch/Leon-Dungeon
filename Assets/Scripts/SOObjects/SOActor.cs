using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SOObjects/SOActor", fileName ="SOActor")]
public class SOActor : ScriptableObject
{
    [Header("Main info")]
    public new LocaleString name;
    public Sprite sprite;
    public ActorStats stats;
    //Brain for AI
    public AIBrain brain;

    [Header("Battle, skills, buffs")]
    public Skill baseAttack;
    public Skill baseDefend;
    public Skill basePassTurn;
    public List<Skill> additionalSkills;

    [Header("Loot")]
    public List<Loot> loot;
}
