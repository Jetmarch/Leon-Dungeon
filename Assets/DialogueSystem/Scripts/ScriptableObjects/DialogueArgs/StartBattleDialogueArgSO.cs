using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName ="StartBattleDialogueArgSO", menuName = "DialogueArgs/StartBattleDialogueArg")]
public class StartBattleDialogueArgSO : DSDialogEventArgSO
{
    [SerializeField] public List<SOActor> enemies;
    [SerializeField] public List<Loot> loot;
}
