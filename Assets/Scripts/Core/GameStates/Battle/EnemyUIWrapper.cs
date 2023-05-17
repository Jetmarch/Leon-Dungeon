using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyUIWrapper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Actor enemy;
    [SerializeField] private bool isTargetChoosingState;
    [SerializeField] private Image enemySprite;
    [SerializeField] private GameObject targetMark;

    private void Awake()
    {
        enemySprite = GetComponent<Image>();
        isTargetChoosingState = false;
        targetMark.SetActive(false);
        GetComponent<Button>().onClick.AddListener(OnSpriteClicked);
    }

    public void SetActor(Actor actor)
    {
        enemy = actor;
        enemySprite.sprite = actor.sprite;
    }

    public Actor GetActor()
    {
        return enemy;
    }

    public void PlayerHasChoseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;
        if(obj.arg.type == SkillType.SelfTarget) return;
        
        isTargetChoosingState = true;
    }

    public void PlayerAbandoneChoosedSkill()
    {
        isTargetChoosingState = false;
    }

    public void OnTargetShow(SOEventArgs e)
    {
        if(!isTargetChoosingState) return;

        var obj = (SOEventArgOne<EnemyUIWrapper>)e;
        if(obj.arg == null || obj.arg == this)
        {
            targetMark.SetActive(true);
        }
        else
        {
            targetMark.SetActive(false);
        }

    }

    public void OnTargetHide(SOEventArgs e)
    {
        targetMark.SetActive(false);
    }

    public void OnSpriteClicked()
    {
        if(!isTargetChoosingState) return;

        SOEventKeeper.Instance.GetEvent("onPointerClickEnemySprite").Raise(new SOEventArgOne<EnemyUIWrapper>(this));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isTargetChoosingState) return;

        SOEventKeeper.Instance.GetEvent("onPointerEnterEnemySprite").Raise(new SOEventArgOne<EnemyUIWrapper>(this));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isTargetChoosingState) return;

        SOEventKeeper.Instance.GetEvent("onPointerExitEnemySprite").Raise(new SOEventArgOne<EnemyUIWrapper>(this));
    }
}
