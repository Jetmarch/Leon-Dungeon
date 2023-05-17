using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
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

    private void Awake()
    {
        baseAttackBtn.GetComponent<Button>().onClick.AddListener(onBaseAttackBtnClick);
        baseDefendBtn.GetComponent<Button>().onClick.AddListener(onBaseDefendBtnClick);
        skillListBtn.GetComponent<Button>().onClick.AddListener(onSkillListBtnClick);
        retreatBtn.GetComponent<Button>().onClick.AddListener(onRetreatBtnClick);
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
        //Animation goes here
        battleScreen.SetActive(true);


        InitEnemyList(eventArg.arg);
        SOEventKeeper.Instance.GetEvent("skillListClose").Raise();
        SOEventKeeper.Instance.GetEvent("onBattleUIReady").Raise();
    }

    public void OnActorTurn(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg.brain == null)
        {
            //Send event for ui animation
            battleControlsParent.SetActive(true);
            return;
        }

        var enemy = FindEnemyByActor(obj.arg);
        StartCoroutine(ChangeColorTest(enemy.GetComponent<Image>()));
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

        var enemy = FindEnemyByActor(obj.arg);
        //TODO: Анимация затемнения спрайта и только после удаление объекта
        enemy.gameObject.SetActive(false);
    }

    public void OnActorDeadAnimationEnd(SOEventArgs e)
    {
        var obj = (SOEventArgOne<EnemyUIWrapper>)e;

        Destroy(obj.arg.gameObject);
    }

    public void OnBattleControlsAnimationEnd()
    {

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
        SOEventKeeper.Instance.GetEvent("skillListOpen").Raise();
    }

    private void onRetreatBtnClick()
    {
        SOEventKeeper.Instance.GetEvent("onPlayerHasChoseSkill").Raise(new SOEventArgOne<Skill>(player.basePassTurn));
    }

    //TODO: onSkillUseEnd - проверяем наличие инициативы у игрока, чтобы понять какие кнопки боя теперь ему доступны
}
