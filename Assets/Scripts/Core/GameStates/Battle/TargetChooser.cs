using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChooser : MonoBehaviour
{
    [SerializeField] private bool isMassTarget;
    public void PlayerHasChoseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;
        isMassTarget = false;

        if (obj.arg.type == SkillType.SelfTarget || obj.arg.type == SkillType.SingleTarget) return;

        isMassTarget = true;
    }

    public void PlayerAbandoneChoosedSkill()
    {
        isMassTarget = false;
    }

    public void OnItemChoose(SOEventArgs e)
    {
        isMassTarget = false;
    }

    public void PointerEnterEnemySprite(SOEventArgs e)
    {
        var obj = (SOEventArgOne<EnemyUIWrapper>)e;
        if (isMassTarget == true)
        {
            //Метку должны отобразить все спрайты
            SOEventKeeper.Instance.GetEvent("onTargetMarkShow").Raise(new SOEventArgOne<EnemyUIWrapper>(null));
        }
        else
        {
            //Метку должен отобразить только один спрайт
            SOEventKeeper.Instance.GetEvent("onTargetMarkShow").Raise(new SOEventArgOne<EnemyUIWrapper>(obj.arg));
        }
    }

    public void PointerExitEnemySprite(SOEventArgs e)
    {
        SOEventKeeper.Instance.GetEvent("onTargetMarkHide").Raise();
    }

    public void PointerClickEnemySprite(SOEventArgs e)
    {
        var obj = (SOEventArgOne<EnemyUIWrapper>)e;
        var list = new List<EnemyUIWrapper>();

        if (isMassTarget)
        {
            list.AddRange(FindObjectsOfType<EnemyUIWrapper>());
            SOEventKeeper.Instance.GetEvent("onPlayerHasChoseTarget").Raise(new SOEventArgOne<List<EnemyUIWrapper>>(list));
        }
        else
        {
            list.Add(obj.arg);
            SOEventKeeper.Instance.GetEvent("onPlayerHasChoseTarget").Raise(new SOEventArgOne<List<EnemyUIWrapper>>(list));
        }

        SOEventKeeper.Instance.GetEvent("onTargetMarkHide").Raise();
        SOEventKeeper.Instance.GetEvent("onPlayerAbandoneChoosedSkill").Raise();
    }
}
