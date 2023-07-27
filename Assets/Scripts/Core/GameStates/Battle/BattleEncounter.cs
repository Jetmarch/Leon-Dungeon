using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEncounter : MonoBehaviour
{
    [SerializeField] private List<SOActor> enemies;

    [SerializeField] private bool useLootFromTheEnemies = true;
    [SerializeField] private List<Loot> loot;
    public void StartBattle()
    {
        SOEventKeeper.Instance.GetEvent("onStartBattle").Raise(new SOEventArgOne<Battle>(new Battle(enemies, loot, useLootFromTheEnemies)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        Debug.Log("Trigger enter");
        StartBattle();
    }
}
