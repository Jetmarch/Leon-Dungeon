using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMaster : MonoBehaviour
{
    //Позже он будет перехватывать события от SkillMaster, когда будет высчитан нанесённый урон/дебафы
    //Чтобы просто отобразить эту информацию с помощью анимаций
    public void OnEnemyUseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<EnemyUIWrapper, Skill>)e;


        StartCoroutine(TestWaitAndRaiseEvent(0.5f, "onEnemyUseSkillEnd"));
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


    private IEnumerator TestWaitAndRaiseEvent(float sec, string eventName)
    {
        yield return new WaitForSeconds(sec);
        SOEventKeeper.Instance.GetEvent(eventName).Raise();
    }
}
