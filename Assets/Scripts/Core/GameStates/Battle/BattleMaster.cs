using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private Battle currentBattle;

    [SerializeField] private Actor currentActorInAction;

    [SerializeField] private bool isWaitingActorTurn;
    [SerializeField] private bool isBattleInProcess;

    //For checking initiative after choosing target
    private Skill choosedSkill;

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }
    public void OnStartBattle(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Battle>)e;
        this.currentBattle = eventArg.arg;
        player.InitiativeReset();

        //Оповещаем UI и только после начинаем считать инициативу
        SOEventKeeper.Instance.GetEvent("onBattleUIInit").Raise(e);
    }

    public void OnActorDead(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;

        if(obj.arg == player)
        {
            Debug.Log("Defeat!");
            SOEventKeeper.Instance.GetEvent("onDefeatInBattle").Raise();
            return;
        }

        if(obj.arg == currentActorInAction)
        {
            SOEventKeeper.Instance.GetEvent("onActorTurnEnd").Raise(new SOEventArgOne<Actor>(obj.arg));
        }


        int countOfDeadEnemies = 0;

        foreach(var enemy in currentBattle.enemies)
        {
            if(enemy.healthStatus.IsDead())
            {
                countOfDeadEnemies++;
            }
        }

        if(countOfDeadEnemies == currentBattle.enemies.Count)
        {
            Debug.Log("Victory!");
            SOEventKeeper.Instance.GetEvent("onVictoryInBattle").Raise();
        }
    }


    //После этого метода начинается считаться инициатива. Вызывается по событию
    public void OnBattleUIReady()
    {
        isBattleInProcess = true;
        isWaitingActorTurn = false;
    }

    public void EndBattle()
    {
        isBattleInProcess = false;
        isWaitingActorTurn = false;
    }

    void Update()
    {
        DoBattle();
    }

    private void DoBattle()
    {
        if(!isBattleInProcess) return;

        DoBattleOnActor(player);

        for(int i = 0; i < currentBattle.enemies.Count; i++)
        {
            //Перебираем каждого противника, пополняем его инициативу, проверяем не заполнилась ли шкала его инициативы
            //Если заполнилась, то управление передаётся ему
            DoBattleOnActor(currentBattle.enemies[i]);
        }
    }

    private void DoBattleOnActor(Actor actor)
    {
        if(isWaitingActorTurn) return;
        if(actor.healthStatus.IsDead()) return;
        if(!actor.healthStatus.CanTakeActions()) return;

        actor.InitiativeStep(Time.deltaTime);

        if (actor.IsReady())
        {
            Debug.Log($"{actor.name.GetValue()} ready!");
            currentActorInAction = actor;
            isWaitingActorTurn = true;
            //Оповещаем об Actor, который ходит
            //Передаём ему управление
            SOEventKeeper.Instance.GetEvent("onActorTurn").Raise(new SOEventArgOne<Actor>(actor));
        }
    }

    public void OnActorTurnAnimationEnd(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;
        obj.arg.UpdateBuffs();
    }

    public void OnActorTurnEnd(SOEventArgs e)
    {
        //if player
        var obj = (SOEventArgOne<Actor>)e;

        //if enemy

        isWaitingActorTurn = false;
    }

    public void OnItemChoosedBattle(SOEventArgs e)
    {
        var obj = (SOEventArgOne<ItemUIWrapper>)e;
        Item choosedItem = obj.arg.GetItem();


        if (!player.HasEnoughInitiative(choosedItem.costInInitiativePercent))
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("Not enought initiative!"));
            Debug.Log("Not enough initiative!");
            return;
        }

        if (!choosedItem.CanUse())
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("Cannot use this item anymore"));
            Debug.Log("Cannot use this item anymore");
            return;
        }

        Debug.Log($"Player use item {choosedItem.name.GetValue()} on self");
        SOEventKeeper.Instance.GetEvent("onPlayerReadyUseItemInBattle").Raise(new SOEventArgOne<Item>(choosedItem));
    }

    public void OnPlayerUsedItemOnSelf(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Item>)e;
        player.ReduceInitiativeOnCost(obj.arg.costInInitiativePercent);
    }

    public void OnPlayerUsedItemOnEnemy(SOEventArgs e)
    {
        var obj = (SOEventArgTwo <List<EnemyUIWrapper>, Item>)e;
        player.ReduceInitiativeOnCost(obj.arg2.costInInitiativePercent);
    }

    public void PlayerHasChoseSkillBattle(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;
        choosedSkill = obj.arg;

        if(choosedSkill == null)
        {
            Debug.LogError("BattleMaster choosedSkill is null!");
        }

        if (!player.HasEnoughInitiative(obj.arg.costInInitiativePercent))
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("Недостаточно инициативы!"));
            Debug.Log("Not enough initiative!");
            return;
        }
        
        SOEventKeeper.Instance.GetEvent("onSkillUseOnSelfInBattle").Raise(new SOEventArgOne<Skill>(obj.arg));
    }

    public void PlayerHasChoseTarget(SOEventArgs e)
    {
        var obj = (SOEventArgOne<List<EnemyUIWrapper>>)e;

        if (!player.HasEnoughInitiative(choosedSkill.costInInitiativePercent))
        {
            SOEventKeeper.Instance.GetEvent("onBattleMessage").Raise(new SOEventArgOne<string>("Недостаточно инициативы!"));
            Debug.Log("Not enough initiative!");
            return;
        }

        SOEventKeeper.Instance.GetEvent("onSkillUseOnEnemyInBattle").Raise(new SOEventArgOne<List<EnemyUIWrapper>>(obj.arg));
    }

    public void PlayerUsedSkillOnSelfBattle(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Skill>)e;
        player.ReduceInitiativeOnCost(obj.arg.costInInitiativePercent);
    }

    public void PlayerUsedSkillOnEnemy(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<List<EnemyUIWrapper>, Skill>)e;
        player.ReduceInitiativeOnCost(obj.arg2.costInInitiativePercent);
    }
}
