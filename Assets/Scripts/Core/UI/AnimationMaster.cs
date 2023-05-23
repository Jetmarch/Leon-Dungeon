using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationMaster : MonoBehaviour
{
    [SerializeField] private Actor player;

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }

    //Позже он будет перехватывать события от SkillMaster, когда будет высчитан нанесённый урон/дебафы
    //Чтобы просто отобразить эту информацию с помощью анимаций
    public void OnEnemyUseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<EnemyUIWrapper, Skill>)e;


        //StartCoroutine(TestWaitAndRaiseEvent(0.5f, "onEnemyUseSkillEnd"));
    }

    public void PlayerHasChoseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;

        if(obj.arg.type != SkillType.SelfTarget) return;

        StartCoroutine(TestWaitAndRaiseEvent(0.5f, "onSkillUseEnd"));
    }

    public void PlayerHasChoseTarget(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<List<EnemyUIWrapper>, Skill>)e;
        
        StartCoroutine(TestWaitAndRaiseEvent(0.5f, "onSkillUseEnd"));
    }

    public void OnActorDead(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg == player)
        {
            //Особая анимация для игрока
            return;
        }
        
        //var enemy = FindEnemyByActor(obj.arg);
        //StartCoroutine(TestActorDeadAnimation(enemy));
    }

    private EnemyUIWrapper FindEnemyByActor(Actor actor)
    {
        var enemyList = FindObjectsOfType<EnemyUIWrapper>();

        foreach(var enemy in enemyList)
        {
            if(enemy.GetComponent<EnemyUIWrapper>().GetActor() == actor)
            {
                return enemy.GetComponent<EnemyUIWrapper>();
            }
        }

        return null;
    }

    private IEnumerator TestWaitAndRaiseEvent(float sec, string eventName)
    {
        yield return new WaitForSeconds(sec);
        SOEventKeeper.Instance.GetEvent(eventName).Raise();
    }
}
