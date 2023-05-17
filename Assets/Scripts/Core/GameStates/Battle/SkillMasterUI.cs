using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMasterUI : MonoBehaviour
{
    [SerializeField] private Skill selectedSkill;
    public void PlayerHasChoseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;
    }
}
