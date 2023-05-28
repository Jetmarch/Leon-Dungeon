using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrainMaster : MonoBehaviour
{
    [SerializeField] private Actor currentEnemyActor;
    [SerializeField] private EnemyUIWrapper currentEnemyUIWrapper;
    [SerializeField] private List<Skill> currentActorSkillSet;
    public void OnActorBuffsUpdated(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg.brain == null) return;

        currentEnemyActor = obj.arg;
        //Из-за того, что переменная Initiative не обновляется в brain
        currentEnemyActor.brain.SetOwner(currentEnemyActor);
        
        currentActorSkillSet = obj.arg.brain.MakeMoveOnFullInitiativeAndGetListOfSkills();

        foreach(var wrapper in FindObjectsOfType<EnemyUIWrapper>())
        {
            if(wrapper.GetActor() == currentEnemyActor)
            {
                currentEnemyUIWrapper = wrapper;
            }
        }

        UseNextSkillInSkillSet();
    }

    public void OnEnemyUseSkillEnd()
    {
        UseNextSkillInSkillSet();
    }

    private void UseNextSkillInSkillSet()
    {
        if(currentActorSkillSet.Count <= 0)
        {
            SOEventKeeper.Instance.GetEvent("onActorTurnEnd").Raise(new SOEventArgOne<Actor>(currentEnemyActor));
            return;
        }

        var nextSkill = currentActorSkillSet[0];
        currentActorSkillSet.Remove(nextSkill);

        SOEventKeeper.Instance.GetEvent("onEnemyUseSkill").Raise(new SOEventArgTwo<EnemyUIWrapper, Skill>(currentEnemyUIWrapper, nextSkill));
    }
}
