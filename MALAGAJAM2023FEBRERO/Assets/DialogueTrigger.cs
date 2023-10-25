using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    DialogueManager manager;

    private void Start()
    {
        manager = FindObjectOfType<DialogueManager>();

        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue);
    }
}
