using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueMaster : MonoBehaviour
{

    [SerializeField] private DSDialogueSO defaultDialogueStart;

    [SerializeField] private DSDialogueSO currentDialogue;

    [SerializeField] private GameObject dialogueScreen;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject answerParent;
    [SerializeField] private GameObject answerPrefab;

    public void OnStartDialogue(SOEventArgs e)
    {
        var obj = (SOEventArgOne<DSDialogueSO>)e;
        currentDialogue = obj.arg;
        ShowCurrentDialogue();
    }

    public void OnNextDialogueNode()
    {
        if (currentDialogue == null) return;

        if (currentDialogue.DialogueType == DS.Enumerations.DSDialogueType.Event)
        {
            Debug.Log("Event!");
            if (currentDialogue.EventArgs == null)
            {
                currentDialogue.SoEvent.Raise();
            }
            else
            {
                currentDialogue.SoEvent.Raise(new SOEventArgOne<DSDialogEventArgSO>(currentDialogue.EventArgs));
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
        dialogueText.text = currentDialogue.Text;
    }

    private void ShowMultipleChoiceDialogue()
    {
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnNextDialogueNode();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            currentDialogue = defaultDialogueStart;
            ShowCurrentDialogue();
        }
    }
}
