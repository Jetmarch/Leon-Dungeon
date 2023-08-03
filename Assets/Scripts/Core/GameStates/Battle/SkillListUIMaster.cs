using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillListUIMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private GameObject skillListParent;
    [SerializeField] private GameObject skillList;
    [SerializeField] private GameObject skillListItemPrefab;
    [SerializeField] private Button closeListBtn;

    [Header("Check conditions")]
    [SerializeField] private bool checkInitiative;
    [SerializeField] private List<SkillType> searchForSkillTypes;


    private void Awake()
    {
        closeListBtn.onClick.AddListener(OnSkillListClose);
    }

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }

    public void OnSkillListToggle()
    {
        if(!skillListParent.activeSelf)
        {
            OnSkillListOpen();
        }
        else
        {
            OnSkillListClose();
        }
    }

    public void OnSkillListOpen()
    {
        if(skillListParent.activeSelf) return;

        skillListParent.SetActive(true);
        RemoveSkillsFromList();
        AddSkillsInList();
    }

    public void OnSkillListClose()
    {
        if(!skillListParent.activeSelf) return;

        RemoveSkillsFromList();
        skillListParent.SetActive(false);
    }

    private void AddSkillsInList()
    {
        foreach(var skill in player.additionalSkills) 
        {
            if (!searchForSkillTypes.Contains(skill.type)) continue;

            var obj = Instantiate(skillListItemPrefab, skillList.transform);
            var wrapper = obj.GetComponent<SkillListItemUIWrapper>();
            wrapper.SetSkill(skill);

            if (checkInitiative)
            {
                if (!player.HasEnoughInitiative(wrapper.GetSkill().costInInitiativePercent))
                {
                    wrapper.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    private void RemoveSkillsFromList()
    {
        foreach(Transform item in skillList.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
