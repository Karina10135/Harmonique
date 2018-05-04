using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is used for note management, whether a note is obtained.

public class NoteManager : MonoBehaviour
{

    public Image[] noteUIimage;
    public Color[] noteColor;

    public bool[] obtainedNote;
    public int currentNoteID;
    public static NoteManager instance;

    private void Start()
    {
        instance = this;
        obtainedNote = new bool[4];
    }


    public void SelectNote(int id)
    {
        currentNoteID = id;
    }

    public void PlayNote()
    {
    }

    public void StopNote()
    {

    }

    public void NoteAvailable(int num)
    {
        obtainedNote[num] = true;
        NoteInputManager.instance.noteAvailable[num] = true;
    }

#region PlayNoteActions

    public void YesNote()
    {
        print("<color=red>Playing First Note</color>");

    }

    public void LightNote()
    {
        print("<color=yellow>Playing Second Note</color>");
    }

    public void BurstNote()
    {
        print("<color=green>Playing Third Note</color>");
    }

    public void NoNote()
    {
        print("<color=purple>Playing Fourth Note</color>");
    }

    public void LastNote()
    {
        print("<color=blue>Playing Fifth Note</color>");
    }

#endregion

}
