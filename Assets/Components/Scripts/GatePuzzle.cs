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
    public bool playing;

    public void StartSequence()
    {
        
    }

    public void CheckNote(int note)
    {
        if (complete) { return; }

        if(note != currentNote) { ResetSequence(); return; }

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
        playing = true;
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
    }

}
