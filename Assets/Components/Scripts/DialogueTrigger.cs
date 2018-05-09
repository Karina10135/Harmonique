using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    private void Start()
    {
        
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.dialogueBox.SetActive(true);
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
