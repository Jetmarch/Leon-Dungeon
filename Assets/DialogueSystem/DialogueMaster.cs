using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueMaster : MonoBehaviour
{
    [SerializeField] private DSDialogueSO currentDialogue;

    [SerializeField] private GameObject dialogueScreen;
    [SerializeField] private TextMeshProUGUI actorName;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject answerParent;
    [SerializeField] private GameObject answerPrefab;

    public void OnStartDialogue(SOEventArgs e)
    {
        var obj = (SOEventArgOne<DSDialogueSO>)e;
        currentDialogue = obj.arg;

        //TODO: Animation of showing dialogue screen here
        dialogueScreen.SetActive(true);

        ShowCurrentDialogue();
    }

    public void OnNextDialogueNode()
    {
        if (currentDialogue == null)
        {
            SOEventKeeper.Instance.GetEvent("onEndDialogue").Raise();
            return;
        }

        if (currentDialogue.DialogueType == DS.Enumerations.DSDialogueType.Event)
        {
            if (currentDialogue.EventArgs == null)
            {
                currentDialogue.SoEvent.Raise();
            }
            else
            {
                CheckTypeOfDialogueEventAndRaiseItWithGameArgs(currentDialogue.SoEvent, currentDialogue.EventArgs);
                //currentDialogue.SoEvent.Raise(new SOEventArgOne<DSDialogEventArgSO>(currentDialogue.EventArgs));
            }
            currentDialogue = currentDialogue.Choices[0].NextDialogue;
        }

        ShowCurrentDialogue();
    }

    public void OnPlayerAnswer(SOEventArgs e)
    {
        var obj = (SOEventArgOne<DSDialogueSO>)e;

        if(obj.arg == null)
        {
            Debug.Log("Dialogue end");
            return;
        }

        currentDialogue = obj.arg;
        ShowCurrentDialogue();
    }

    public void OnEndDialogue()
    {
        //TODO: Animation here
        dialogueScreen.SetActive(false);
    }

    public void OnSpaceKeyDown()
    {
        OnNextDialogueNode();
    }

    public void OnStartBattle()
    {
        dialogueScreen.SetActive(false);
    }
    private void CheckTypeOfDialogueEventAndRaiseItWithGameArgs(SOEvent ev, DSDialogEventArgSO evArgs)
    {
        if(evArgs is StartBattleDialogueArgSO)
        {
            var startBattleArg = (StartBattleDialogueArgSO)evArgs;
            ev.Raise(new SOEventArgOne<Battle>(new Battle(startBattleArg.enemies, startBattleArg.loot)));
        }
        else
        {
            ev.Raise(new SOEventArgOne<DSDialogEventArgSO>(evArgs));
        }
    }

    private void ShowCurrentDialogue()
    {
        ClearChoices();
        switch (currentDialogue.DialogueType)
        {
            case DS.Enumerations.DSDialogueType.SingleChoice:
                ShowSingleChoiceDialogue();

                currentDialogue = currentDialogue.Choices[0].NextDialogue;
                break;
            case DS.Enumerations.DSDialogueType.MultipleChoice:
                ShowMultipleChoiceDialogue();
                break;
            //case DS.Enumerations.DSDialogueType.Event:

            //    break;
        }
    }

    private void ShowSingleChoiceDialogue()
    {
        actorName.text = currentDialogue.NodeActor.name.GetValue();
        dialogueText.text = currentDialogue.Text;
    }

    private void ShowMultipleChoiceDialogue()
    {
        actorName.text = currentDialogue.NodeActor.name.GetValue();
        dialogueText.text = currentDialogue.Text;
        ShowChoices();
    }

    private void ShowChoices()
    {
        foreach(var choice in currentDialogue.Choices)
        {
            var obj = Instantiate(answerPrefab, answerParent.transform);
            obj.GetComponent<AnswerController>().SetNextDialogueNode(choice.NextDialogue);
            obj.GetComponent<AnswerController>().SetAnswerText(choice.Text);
        }
    }

    private void ClearChoices()
    {
        foreach(Transform answer in answerParent.transform)
        {
            Destroy(answer.gameObject);
        }
    }
}
