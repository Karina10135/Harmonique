using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePuzzle : MonoBehaviour
{

    public int[] noteSequence;
    public int currentNote;
    public int noteNum;
    public bool complete;

    public void CheckNote(int note)
    {
        if (complete) { return; }

        if(note != currentNote) { ResetSequence(); return; }

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
        noteNum = 0;
        AssignNote();
    }

    public void AssignNote()
    {
        currentNote = noteSequence[noteNum];

    }


    public void CompletedPuzzle()
    {
        print("Game Complete");
    }

}
