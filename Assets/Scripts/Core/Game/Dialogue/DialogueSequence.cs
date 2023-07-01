using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueSequence", menuName = "DialogueSequence")]
public class DialogueSequence : ScriptableObject
{
    public string dialogueName;
    [TextArea]
    public string dialogueNote;

    List<DialogueNode> nodes = new List<DialogueNode>();
}
