using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    

    public Image[] moleCharacter;
    public int[] moleID;

    public int currentNote;
    public int notes;

    public Color[] noteCol;

    bool[] IDtaken;

    NoteManager note;

    private void Start()
    {
        note = NoteManager.instance;

        moleID = new int[4];
        IDtaken = new bool[4];
        AssignMoles();
        NextNote();
    }

    public void CompareValues()
    {

    }

    public void NextNote()
    {

        foreach (Image charac in moleCharacter)
        {
            charac.color = Color.gray;
        }

        moleCharacter[currentNote].color = noteCol[moleID[currentNote]];

        
        
    }

    public void AssignMoles()
    {
       

        for (int i = 0; i< moleID.Length; i++)
        {
            moleID[i] = Random.RandomRange(0, 3);
        }
    }


    public void CompleteNote()
    {
        currentNote++;
        if(currentNote == notes)
        {
            CompletedPuzzle();
        }
        else
        {
            NextNote();
        }
    }

    public void FailedNote()
    {
        currentNote = 0;
        print("Try Again!");

    }

    public void CompletedPuzzle()
    {
        print("Finished Sequence");
    }





}
