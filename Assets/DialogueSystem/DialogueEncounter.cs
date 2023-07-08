using DS;
using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEncounter : MonoBehaviour
{
    [SerializeField] private DSDialogueContainerSO dialogue;

    [SerializeField] private List<string> groupNames;

    [SerializeField] private List<string> startDialogueName;

    private void Start()
    {
        
    }
}
