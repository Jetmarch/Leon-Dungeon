using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour
{
    [SerializeField] private DSDialogueSO nextDialogueNode;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private SOEvent onPlayerAnswer;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnPlayerAnswer);
    }

    public void SetNextDialogueNode(DSDialogueSO nextNode)
    {
        nextDialogueNode = nextNode;
    }

    public void SetAnswerText(string text)
    {
        answerText.text = text;
    }

    public DSDialogueSO GetNextDialogueNode()
    {
        return nextDialogueNode;
    }

    private void OnPlayerAnswer()
    {
        SOEventKeeper.Instance.GetEvent("onPlayerAnswer").Raise(new SOEventArgOne<DSDialogueSO>(nextDialogueNode));
    }
}
