using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.UI;

public class DialogueMaster : MonoBehaviour
{
    [SerializeField] private DSDialogueSO currentDialogue;

    [SerializeField] private GameObject dialogueScreen;
    [SerializeField] private Image actorSprite;
    [SerializeField] private TextMeshProUGUI actorName;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject answerParent;
    [SerializeField] private GameObject answerPrefab;

    [SerializeField] private MMF_Player startDialogue;
    [SerializeField] private MMF_Player endDialogue;

    [SerializeField] private bool isInDialogue;

    private void Awake()
    {
        startDialogue?.Initialization();
    }

    public void OnStartDialogue(SOEventArgs e)
    {
        var obj = (SOEventArgOne<DSDialogueSO>)e;
        currentDialogue = obj.arg;
        //TODO: Animation of showing dialogue screen here
        
        actorName.text = string.Empty;
        dialogueText.text = string.Empty;
        startDialogue?.PlayFeedbacks();

        isInDialogue = true;
    }

    public void OnStartDialogueAnimationEnd()
    {
        ShowCurrentDialogue();

        actorSprite.gameObject.SetActive(true);
        actorSprite.preserveAspect = true;
    }

    public void OnNextDialogueNode()
    {
        if (!isInDialogue) return;

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

            if (currentDialogue.Choices[0].NextDialogue != null)
            {
                currentDialogue = currentDialogue.Choices[0].NextDialogue;
            }
            else
            {
                SOEventKeeper.Instance.GetEvent("onEndDialogue").Raise();
                return;
            }
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
        //dialogueScreen.SetActive(false);
        endDialogue?.PlayFeedbacks();
        actorSprite.gameObject.SetActive(false);
        isInDialogue = false;
    }

    public void OnEndDialogueAnimationEnd()
    {

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
            ev.Raise(new SOEventArgOne<Battle>(new Battle(startBattleArg.enemies, startBattleArg.loot, startBattleArg.useLootFromTheEnemies)));
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
        UpdateDialogueTextAndSprite();
    }

    private void ShowMultipleChoiceDialogue()
    {
        UpdateDialogueTextAndSprite();
        ShowChoices();
    }

    private void UpdateDialogueTextAndSprite()
    {
        actorSprite.sprite = currentDialogue.NodeActor.sprite;
        actorName.text = currentDialogue.NodeActor.name.GetValue();
        dialogueText.text = currentDialogue.Text;
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
