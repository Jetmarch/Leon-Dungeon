using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChooser : MonoBehaviour
{
    [SerializeField] private Skill selectedSkill;
    public void PlayerHasChoseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e; 

        if(obj.arg.type == SkillType.SelfTarget) return;

        selectedSkill = obj.arg;
    }

    public void PlayerAbandoneChoosedSkill()
    {
        selectedSkill = null;
    }

    public void PointerEnterEnemySprite(SOEventArgs e)
    {
        var obj = (SOEventArgOne<EnemyUIWrapper>)e;
        if(selectedSkill.type == SkillType.MassTarget)
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

         if(selectedSkill.type == SkillType.MassTarget)
        {
            list.AddRange(FindObjectsOfType<EnemyUIWrapper>());
            SOEventKeeper.Instance.GetEvent("onPlayerHasChoseTarget").Raise(new SOEventArgTwo<List<EnemyUIWrapper>, Skill>(list, selectedSkill));
        }
        else
        {
            list.Add(obj.arg);
            SOEventKeeper.Instance.GetEvent("onPlayerHasChoseTarget").Raise(new SOEventArgTwo<List<EnemyUIWrapper>, Skill>(list, selectedSkill));
        }

        SOEventKeeper.Instance.GetEvent("onTargetMarkHide").Raise();
        SOEventKeeper.Instance.GetEvent("onPlayerAbandoneChoosedSkill").Raise();
    }
}
