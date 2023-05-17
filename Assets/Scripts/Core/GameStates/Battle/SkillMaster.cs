using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private Skill selectedSkill;

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }

    public void PlayerHasChoseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;

        if(obj.arg.type != SkillType.SelfTarget) return;

        if(!player.HasEnoughInitiative(obj.arg))
        {
            Debug.Log("Not enough initiative!");
            return;
        }

        Debug.Log($"Player use skill {obj.arg.name.GetValue()} on self");
        player.ReduceInitiativeOnSkillCost(obj.arg);
        obj.arg.Use(player, player);
    }

    public void PlayerHasChoseTarget(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<List<EnemyUIWrapper>, Skill>)e;
        
        if(!player.HasEnoughInitiative(obj.arg2))
        {
            Debug.Log("Not enough initiative!");
            return;
        }

        player.ReduceInitiativeOnSkillCost(obj.arg2);

        foreach(var enemy in obj.arg1)
        {
            Debug.Log($"Player use skill {obj.arg2.name.GetValue()} on {enemy.GetActor().name.GetValue()}");
            obj.arg2.Use(enemy.GetActor(), player);
        }
        
    }
}
