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

    public void PlayerHasChoseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;

        if(obj.arg.type != SkillType.SelfTarget)
        {
            choosedSkill = obj.arg;
            return;
        }

        if(!player.HasEnoughInitiative(obj.arg.costInInitiativePercent))
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("Недостаточно инициативы!"));
            Debug.Log("Not enough initiative!");
            return;
        }

        Debug.Log($"Player use skill {obj.arg.name.GetValue()} on self");
        player.ReduceInitiativeOnCost(obj.arg.costInInitiativePercent);
        obj.arg.Use(player, player);
        //TODO: Raise event onPlayerUsedSkillOnSelf
    }

    public void PlayerHasChoseTarget(SOEventArgs e)
    {
        if (choosedSkill == null) return;

        var obj = (SOEventArgOne<List<EnemyUIWrapper>>)e;
        
        if(!player.HasEnoughInitiative(choosedSkill.costInInitiativePercent))
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("Недостаточно инициативы!"));
            Debug.Log("Not enough initiative!");
            return;
        }

        player.ReduceInitiativeOnCost(choosedSkill.costInInitiativePercent);

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
        obj.arg2.Use(player, obj.arg1.GetActor());
    }

    public void OnItemChoosed(SOEventArgs e)
    {
        choosedSkill = null;
    }
}
