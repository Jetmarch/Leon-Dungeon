using UnityEngine;

namespace DS
{
    using ScriptableObjects;

    public class DSDialogue : MonoBehaviour
    {
        /* Dialogue Scriptable Objects */
        [SerializeField] private DSDialogueContainerSO dialogueContainer;
        [SerializeField] private DSDialogueGroupSO dialogueGroup;
        [SerializeField] private DSDialogueSO dialogue;

        /* Filters */
        [SerializeField] private bool groupedDialogues;
        [SerializeField] private bool startingDialoguesOnly;

        /* Indexes */
        [SerializeField] private int selectedDialogueGroupIndex;
        [SerializeField] private int selectedDialogueIndex;

        public DSDialogueSO GetDialogueContainer()
        {
            return dialogue;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            Debug.Log("Trigger enter");
            StartDialogue();
        }

        private void StartDialogue()
        {
            SOEventKeeper.Instance.GetEvent("onStartDialogue").Raise(new SOEventArgOne<DSDialogueSO>(dialogue));
        }
    }
}