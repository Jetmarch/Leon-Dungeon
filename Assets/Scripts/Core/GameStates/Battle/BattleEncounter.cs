using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEncounter : MonoBehaviour
{
    [SerializeField] private List<SOActor> enemies;
    [SerializeField] private List<SOItem> items;
    public void StartBattle()
    {
        SOEventKeeper.Instance.GetEvent("startBattle").Raise(new SOEventArgOne<Battle>(new Battle(enemies, items)));
    }

    private void Start() {
        StartBattle();
    }

}
