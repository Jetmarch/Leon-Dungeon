using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName ="StartBattleDialogueArgSO", menuName = "DialogueArgs/StartBattleDialogueArg")]
public class StartBattleDialogueArgSO : DSDialogEventArgSO
{
    public List<SOActor> enemies;
    public List<Loot> loot;
    public bool useLootFromTheEnemies = true;
}
