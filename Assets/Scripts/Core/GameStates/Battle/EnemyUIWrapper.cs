using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;
using DG.Tweening;

public class EnemyUIWrapper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Actor enemy;
    [SerializeField] private bool isTargetChoosingState;
    [SerializeField] private Image borderSprite;
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
    [SerializeField] private MMF_Player idleAnimation;

    [Header("Buffs")]
    [SerializeField] private GameObject buffList;
    [SerializeField] private GameObject buffItemPrefab;

    [SerializeField] private float attackPower;
    [SerializeField] private float attackAnimationDurationInSec;
    [SerializeField] private float attackEllactisity;
    [SerializeField] private int attackVibrato;

    private void Awake()
    {
        isTargetChoosingState = false;
        targetMark.SetActive(false);
        GetComponent<Button>().onClick.AddListener(OnSpriteClicked);
    }

    private void Start()
    {
        idleAnimation?.PlayFeedbacks();
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
        targetMark.SetActive(false);
    }

    public void OnItemChoose(SOEventArgs e)
    {
        var obj = (SOEventArgOne<ItemUIWrapper>)e;
        if (obj.arg.GetItem().type == ItemType.UsableOnSelf) return;

        isTargetChoosingState = true;
    }

    public void OnItemAbandone()
    {
        isTargetChoosingState = false;
        targetMark.SetActive(false);
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

    public void PlayerUsedSkillOnEnemy(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<List<EnemyUIWrapper>, Skill>)e;

         if(obj.arg1.Contains(this))
         {
             hitAnimation?.PlayFeedbacks();
         }

         UpdateBuffList();
    }

    public void OnActorTurn(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg == enemy)
        {
            startTurnAnimation?.PlayFeedbacks();
        }

        UpdateBuffList();
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

        if (obj.arg1 != this) return;
        if (obj.arg1.GetActor().healthStatus.IsDead() || !obj.arg1.GetActor().healthStatus.CanTakeActions()) return;

        if (!obj.arg1.GetActor().HasEnoughInitiative(obj.arg2.costInInitiativePercent)) return;

        enemySprite.rectTransform.DOPunchPosition(Vector3.down * attackPower, attackAnimationDurationInSec, attackVibrato, attackEllactisity).OnComplete(EnemyUseSkillEnd);
    }

    public void OnEnemyHealthChanged(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<Actor, float>)e;
        if(obj.arg1 != enemy) return;

        //TODO: Animation with floating value here
        hitAnimation?.PlayFeedbacks();
        UpdateBuffList();
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
        SOEventKeeper.Instance.GetEvent("onActorTurnAnimationEnd").Raise(new SOEventArgOne<Actor>(enemy));
    }

    private void UpdateBuffList()
    {
        EmptyBuffList();
        FillBuffList();
    }

    private void EmptyBuffList()
    {
        foreach(Transform obj in buffList.transform)
        {
            Destroy(obj.gameObject);
        }
    }

    private void FillBuffList()
    {
        foreach(var buff in enemy.buffs)
        {
            var obj = Instantiate(buffItemPrefab, buffList.transform);
            obj.GetComponent<Image>().sprite = buff.icon;
        }
    }
}
