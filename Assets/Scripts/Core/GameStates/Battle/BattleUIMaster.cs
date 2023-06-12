using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private Battle currentBattle;
    [SerializeField] private GameObject battleScreen;

    [Header("Enemies")]
    [SerializeField] private GameObject enemyListParent;
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private GameObject enemyWrapperPrefab;

    [Header("Battle controls")]
    [SerializeField] private GameObject battleControlsParent;
    [SerializeField] private GameObject baseAttackBtn;
    [SerializeField] private GameObject baseDefendBtn;
    [SerializeField] private GameObject skillListBtn;
    [SerializeField] private GameObject retreatBtn;
    [SerializeField] private GameObject inventoryBtn;

    private void Awake()
    {
        baseAttackBtn.GetComponent<Button>().onClick.AddListener(onBaseAttackBtnClick);
        baseDefendBtn.GetComponent<Button>().onClick.AddListener(onBaseDefendBtnClick);
        skillListBtn.GetComponent<Button>().onClick.AddListener(onSkillListBtnClick);
        retreatBtn.GetComponent<Button>().onClick.AddListener(onRetreatBtnClick);
        inventoryBtn.GetComponent<Button>().onClick.AddListener(onInventoryBtnClick);
        battleControlsParent.SetActive(false);
    }

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }

    public void OnBattleUIInit(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Battle>)e;
        SOEventKeeper.Instance.GetEvent("onSkillListClose").Raise();
        //Animation goes here
        currentBattle = eventArg.arg;

        battleControlsParent.SetActive(false);
        battleScreen.SetActive(true);
        SOEventKeeper.Instance.GetEvent("onBattleStartAnimation").Raise();
    }

    public void OnBattleStartAnimationEnd()
    {
        //battleScreen.SetActive(false);
        InitEnemyList(currentBattle);
        
        SOEventKeeper.Instance.GetEvent("onBattleUIReady").Raise();
    }

    public void OnActorTurn(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg.brain == null)
        {
            ShowBattleControls();
            return;
        }

        // var enemy = FindEnemyByActor(obj.arg);
        // StartCoroutine(ChangeColorTest(enemy.GetComponent<Image>()));
        // SOEventKeeper.Instance.GetEvent("onEnemyActorTurnAnimationEnd").Raise(new SOEventArgOne<Actor>(obj.arg));
    }

    public void OnActorTurnEnd(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;
        if(obj.arg.brain == null)
        {
            //Send event for ui animation
            battleControlsParent.SetActive(false);
        }
        //TODO: Battle messages 
    }

    public void OnActorDead(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg == player)
        {
            Debug.Log("Player is dead!");
            return;
        }
    }

    public void OnActorDeadAnimationEnd(SOEventArgs e)
    {
        var obj = (SOEventArgOne<EnemyUIWrapper>)e;

        obj.arg.gameObject.SetActive(false);
    }

    //TODO: onSkillUseEnd - проверяем наличие инициативы у игрока, чтобы понять какие кнопки боя теперь ему доступны
    public void OnSkillUseEnd(SOEventArgs e)
    {
        if(!player.HasEnoughInitiative(player.baseAttack.costInInitiativePercent))
        {
            baseAttackBtn.GetComponent<Button>().interactable = false;
        }
        if(!player.HasEnoughInitiative(player.baseDefend.costInInitiativePercent))
        {
            baseDefendBtn.GetComponent<Button>().interactable = false;
        }
    }

    public void OnBattleControlsAnimationEnd()
    {

    }

    public void OnVictoryAnimationEnd()
    {
        //battleScreen.SetActive(false);
        battleControlsParent.SetActive(false);
    }

    public void OnDefeatAnimationEnd()
    {
        SOEventKeeper.Instance.GetEvent("onBattleEndAnimation").Raise();
    }

    public void OnRewardReceived()
    {
        SOEventKeeper.Instance.GetEvent("onBattleEndAnimation").Raise();
    }

    public void OnBattleEndAnimationEnd()
    {
        battleScreen.SetActive(false);
    }

    private EnemyUIWrapper FindEnemyByActor(Actor actor)
    {
        foreach(var enemy in enemyList)
        {
            if(enemy.GetComponent<EnemyUIWrapper>().GetActor() == actor)
            {
                return enemy.GetComponent<EnemyUIWrapper>();
            }
        }

        return null;
    }

    private void ShowBattleControls()
    {
        //Send event for ui animation
        battleControlsParent.SetActive(true);
        if(player.HasEnoughInitiative(player.baseAttack.costInInitiativePercent))
        {
            baseAttackBtn.GetComponent<Button>().interactable = true;
        }
        if(player.HasEnoughInitiative(player.baseDefend.costInInitiativePercent))
        {
            baseDefendBtn.GetComponent<Button>().interactable = true;
        }
    }

    //Just for testing. Delete it later
    private IEnumerator ChangeColorTest(Image image)
    {
        image.color = Color.black;
        yield return new WaitForSeconds(1f);
        image.color = Color.white;
    }

    private void InitEnemyList(Battle battle)
    {
        //Очищаем лист от предыдущих противников, если они были
        foreach(Transform prevEnemy in enemyListParent.transform)
        {
            Destroy(prevEnemy.gameObject);
        }

        foreach(var enemy in battle.enemies)
        {
            AddEnemyInEnemyList(enemy);
        }
    }

    private void AddEnemyInEnemyList(Actor actor)
    {
        var obj = Instantiate(enemyWrapperPrefab, enemyListParent.transform);
        obj.GetComponent<EnemyUIWrapper>().SetActor(actor);
        enemyList.Add(obj);
    }

    private void onBaseAttackBtnClick()
    {
        SOEventKeeper.Instance.GetEvent("onPlayerHasChoseSkill").Raise(new SOEventArgOne<Skill>(player.baseAttack));
    }

    private void onBaseDefendBtnClick()
    {
        SOEventKeeper.Instance.GetEvent("onPlayerHasChoseSkill").Raise(new SOEventArgOne<Skill>(player.baseDefend));
    }

    private void onSkillListBtnClick()
    {
        SOEventKeeper.Instance.GetEvent("onSkillListOpen").Raise();
    }

    private void onRetreatBtnClick()
    {
        SOEventKeeper.Instance.GetEvent("onPlayerHasChoseSkill").Raise(new SOEventArgOne<Skill>(player.basePassTurn));
    }

    private void onInventoryBtnClick()
    {
        SOEventKeeper.Instance.GetEvent("onInventoryOpen").Raise();
    }
}
