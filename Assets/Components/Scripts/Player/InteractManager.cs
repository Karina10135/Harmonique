using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{



    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                other.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                print("dig");

                if(other.gameObject.GetComponent<DialogueTrigger>().dialogue.name == "Mole Guard")
                {
                    other.gameObject.GetComponent<MoleGuard>().SpeakTo(NoteManager.instance.currentNoteID);
                }
                return;

            }
        }

        if (other.gameObject.CompareTag("Moles"))
        {

            if (Input.GetMouseButtonDown(0))
            {
                PuzzleManager.instance.moles.StartPuzzle();
                return;

            }


        }

        if (other.gameObject.CompareTag("Gate"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(PuzzleManager.instance.gate.playing == false)
                {
                    PuzzleManager.instance.gate.ResetSequence();
                    return;

                }
                else { return; }
            }
        }
    }
}
