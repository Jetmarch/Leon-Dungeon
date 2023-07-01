using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEncounter : MonoBehaviour
{
    //[SerializeField] private DialogueSequence dialogue;

    public void StartDialogue()
    {
        SOEventKeeper.Instance.GetEvent("onStartDialogue").Raise(new SOEventArgOne<DialogueSequence>(dialogue));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        StartDialogue();
    }
}
