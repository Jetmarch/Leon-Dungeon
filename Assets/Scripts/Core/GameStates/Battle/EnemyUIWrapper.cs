using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;

public class EnemyUIWrapper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Actor enemy;
    [SerializeField] private bool isTargetChoosingState;
    [SerializeField] private Image enemySprite;
    [SerializeField] private GameObject targetMark;

    [Header("Animation feedbacks")]
    [SerializeField] private MMF_Player arriveAnimation;
    [SerializeField] private MMF_Player startTurnAnimation;
    [SerializeField] private MMF_Player hitAnimation;
    [SerializeField] private MMF_Player attackAnimation;
    [SerializeField] private MMF_Player deathAnimation;
    [SerializeField] private MMF_Player targetShowAnimation;
    [SerializeField] private MMF_Player targetHideAnimation;

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
            targetShowAnimation?.PlayFeedbacks();
            //targetMark.SetActive(true);
        }
        else
        {
            //targetHideAnimation?.PlayFeedbacks();
            targetMark.SetActive(false);
        }

    }

    public void OnTargetHide(SOEventArgs e)
    {
        //targetHideAnimation?.PlayFeedbacks();
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

    public void PlayerHasChoseTarget(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<List<EnemyUIWrapper>, Skill>)e;

        if(obj.arg1.Contains(this))
        {
            hitAnimation?.PlayFeedbacks();
        }
    }

    public void OnActorTurn(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg == enemy)
        {
            startTurnAnimation?.PlayFeedbacks();
        }
    }

    public void OnActorDead(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg == enemy)
        {
            deathAnimation?.PlayFeedbacks();
        }
    }

    public void OnEnemyUseSkill(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<EnemyUIWrapper, Skill>)e;

        if(obj.arg1 == this)
        {
            attackAnimation?.PlayFeedbacks();
        }
    }

    public void ActorDeadAnimationEnd()
    {
        SOEventKeeper.Instance.GetEvent("onActorDeadAnimationEnd").Raise(new SOEventArgOne<EnemyUIWrapper>(this));
    }

    public void EnemyUseSkillEnd()
    {
        SOEventKeeper.Instance.GetEvent("onEnemyUseSkillEnd").Raise();
    }

    public void EnemyActorTurnAnimationEnd()
    {
        SOEventKeeper.Instance.GetEvent("onEnemyActorTurnAnimationEnd").Raise(new SOEventArgOne<Actor>(enemy));
    }
}
