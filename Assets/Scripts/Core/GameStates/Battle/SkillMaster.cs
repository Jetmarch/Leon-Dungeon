using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    private Skill choosedSkill;
    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }

    public void SkillUseOnSelfInBattle(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;

        if(obj.arg.type != SkillType.SelfTarget)
        {
            choosedSkill = obj.arg;
            return;
        }

        Debug.Log($"Player use skill {obj.arg.name.GetValue()} on self");
        obj.arg.Use(player, player);
        //TODO: Raise event onPlayerUsedSkillOnSelf
        SOEventKeeper.Instance.GetEvent("onPlayerUsedSkillOnSelfBattle").Raise(new SOEventArgOne<Skill>(obj.arg));
    }

    public void SkillUseOnSelfInTravel(SOEventArgs e)
    {
        //TODO: Решить проблему с дубликацией кода
        var obj = (SOEventArgOne<Skill>)e;

        if (obj.arg.type != SkillType.SelfTarget)
        {
            choosedSkill = obj.arg;
            return;
        }

        Debug.Log($"Player use skill {obj.arg.name.GetValue()} on self");
        obj.arg.Use(player, player);
        //TODO: Raise event onPlayerUsedSkillOnSelf
        SOEventKeeper.Instance.GetEvent("onPlayerUsedSkillOnSelfTravel").Raise(new SOEventArgOne<Skill>(obj.arg));
    }

    public void SkillUseOnEnemyInBattle(SOEventArgs e)
    {
        if (choosedSkill == null) return;

        var obj = (SOEventArgOne<List<EnemyUIWrapper>>)e;
        
        foreach(var enemy in obj.arg)
        {
            Debug.Log($"Player use skill {choosedSkill.name.GetValue()} on {enemy.GetActor().name.GetValue()}");
            choosedSkill.Use(enemy.GetActor(), player);
        }

        SOEventKeeper.Instance.GetEvent("onPlayerUsedSkillOnEnemy").Raise(new SOEventArgTwo<List<EnemyUIWrapper>, Skill>(obj.arg, choosedSkill));

        choosedSkill = null;
    }

    public void OnEnemyUseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<EnemyUIWrapper, Skill>)e;

        if(obj.arg1.GetActor().healthStatus.IsDead() || !obj.arg1.GetActor().healthStatus.CanTakeActions())
        {
            Debug.Log("Enemy is dead or stunned!");
            SOEventKeeper.Instance.GetEvent("onEnemyUseSkillEnd").Raise();
            return;
        }

        if(!obj.arg1.GetActor().HasEnoughInitiative(obj.arg2.costInInitiativePercent))
        {
            Debug.Log("Enemy has not enough initiative!");
            SOEventKeeper.Instance.GetEvent("onEnemyUseSkillEnd").Raise();
            return;
        }

        obj.arg1.GetActor().ReduceInitiativeOnCost(obj.arg2.costInInitiativePercent);

        Debug.Log($"Enemy use skill {obj.arg2.name.GetValue()} on {player.name.GetValue()}");

        SOEventKeeper.Instance.GetEvent("onEnemyUseSkillAnimation").Raise(obj);

        obj.arg2.Use(player, obj.arg1.GetActor());
    }

    public void OnItemChoosed(SOEventArgs e)
    {
        choosedSkill = null;
    }

    public void OnEscapeKeyDown()
    {
        choosedSkill = null;
        SOEventKeeper.Instance.GetEvent("onPlayerAbandoneChoosedSkill").Raise();
    }
}
