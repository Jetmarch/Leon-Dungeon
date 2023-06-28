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
       
    }
}
