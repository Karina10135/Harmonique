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
    public static NoteManager instance;

    private void Start()
    {
        instance = this;
        obtainedNote = new bool[4];
    }


    public void SelectNote()
    {

    }

    public void NoteAvailable(int num)
    {
        obtainedNote[num] = true;
        NoteInputManager.instance.noteAvailable[num] = true;
    }



}
