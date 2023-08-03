using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillListItemUIWrapper : MonoBehaviour
{
    [SerializeField] private Skill skill;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private string playerHasChoseSkillEventName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayerChoseSkill);        
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
        skillIcon.sprite = skill.icon;
        skillName.text = skill.name.GetValue();
    }

    public Skill GetSkill()
    {
        return skill;
    }

    public void PlayerChoseSkill()
    {
        SOEventKeeper.Instance.GetEvent(playerHasChoseSkillEventName).Raise(new SOEventArgOne<Skill>(skill));
        Debug.Log($"Player has choose skill {skill.name.GetValue()}");
    }
}
