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
        if(DialogueManager.instance.dialogue == true) { return; }
        DialogueManager.instance.StartDialogue(dialogue);
    }

    
}
