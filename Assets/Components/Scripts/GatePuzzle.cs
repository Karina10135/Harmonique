using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePuzzle : MonoBehaviour
{
    public GameObject confete;
    public GameObject[] trumpets;
    public int[] noteSequence;

    public int currentNote;
    public int noteNum;
    public bool complete;
    public Animator anim;
    public NoteManager noteManager;

    private void Start()
    {
        noteManager = NoteManager.instance;
        anim = GetComponent<Animator>();
    }

    public void StartSequence()
    {
        
    }

    public void CheckNote(int note)
    {
        if (complete) { return; }
        print("Playing to gate");


        if (note != currentNote) { ResetSequence(); return; }

        PlayTrumpet(trumpets[note].transform);

        if(noteNum == 4)
        {
            CompletedPuzzle();
            return;
        }

        noteNum++;
        AssignNote();

    }

    public void ResetSequence()
    {
        print("Wrong note!");
        noteNum = 0;
        AssignNote();
    }

    public void AssignNote()
    {

        currentNote = noteSequence[noteNum];
        print(currentNote);
    }

    public void PlayTrumpet(Transform position)
    {
        GameObject trumpet = Instantiate(confete, position);
        trumpet.GetComponentInChildren<ParticleSystem>().Play();
    }


    public void CompletedPuzzle()
    {
        print("Game Complete");
        Fabric.EventManager.Instance.PostEvent("Misc/Melodygatesuccess", gameObject);
        anim.SetBool("Complete", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            noteManager.gateRelay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            noteManager.gateRelay = false;
        }
    }

}
