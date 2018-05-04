using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    

    public GameObject[] moleCharacter;
    public int[] moleID;


    public int currentNote;
    public int notes;

    private void Start()
    {
        moleID = new int[4];
    }

    public void CompareValues()
    {

    }

    public void NextNote()
    {
        AssignMoleNotes();

        foreach (GameObject charac in moleCharacter)
        {
            charac.SetActive(false);
        }

        moleCharacter[currentNote].SetActive(true);
        
    }

    public void AssignMoleNotes()
    {
        for(int i = 0; i < moleID.Length; i++)
        {
            moleID[i] = (Random.Range(0, 4));
            print(moleID[i]);
        }
    }

    public void CompleteNote()
    {
        currentNote++;
        if(currentNote == notes)
        {
            CompletedPuzzle();
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
