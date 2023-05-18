using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    [SerializeField] private Actor player;

    [SerializeField] private BattleEncounter battleEncounter;

    [SerializeField] private Actor currentActorOnTurn;

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }

    public void OnActorTurn(SOEventArgs e)
    {
        var actor = ((SOEventArgOne<Actor>)e).arg;
        currentActorOnTurn = actor;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentActorOnTurn.InitiativeReset();
            SOEventKeeper.Instance.GetEvent("onActorTurnEnd").Raise(new SOEventArgOne<Actor>(currentActorOnTurn));
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            currentActorOnTurn.InitiativeReset();
            SOEventKeeper.Instance.GetEvent("attackAnimationEnd").Raise();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            battleEncounter.StartBattle();
        }
    }
}
