using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Mole Guard")
        {
            NoteManager.instance.InteractableObject(other.gameObject.tag);
            print("guard");
        }

        if(other.gameObject.CompareTag("NPC"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                other.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();

            }
            print("encountered npc");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                other.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                print("dig");
            }
        }
    }
}
