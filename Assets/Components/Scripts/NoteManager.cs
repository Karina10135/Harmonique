﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is used for note management, whether a note is obtained.

public class NoteManager : MonoBehaviour
{


    public int currentNoteID;
    public bool selectingNote;

    //UI vars
    public GameObject noteUI;
    public Image selectedNoteImage;
    public Image[] noteUIimage;
    public Color[] noteColor;


    public bool[] obtainedNote;
    public string interactObject;

    public LightNote light;
    public BurstNote burst;
    public NoteInputManager note;
    public TriggerSecondNote secNote;
    public static NoteManager instance;

    private void Start()
    {
        instance = this;
        obtainedNote = new bool[5];
        obtainedNote[0] = true;
    }

    private void Update()
    {
        ProcessInput();
    }

    public void SelectNote(int id)
    {
        currentNoteID = id;
        selectedNoteImage.color = noteColor[id];
        noteUIimage[id].color = noteColor[id];
        noteUI.SetActive(false);
    }

    public void PlayNote()
    {
        if(currentNoteID == 0)
        {
            YesNote();
        }
        if (currentNoteID == 1)
        {
            LightNote();
        }
        if (currentNoteID == 2)
        {
            BurstNote();
        }
        if (currentNoteID == 3)
        {
            NoNote();
        }
        if (currentNoteID == 4)
        {
            LastNote();
        }

    }

    public void StopNote()
    {
        if(currentNoteID == 0 && obtainedNote[1] == false)
        {
            secNote.ResetTrigger();
        }

        if(currentNoteID == 1)
        {
            light.LightTrigger(false);
        }
    }

    public void ProcessInput()
    {
        if(GameManager.GM.dialog == true) { return; }

        if (selectingNote == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (obtainedNote[0] == true)
                {
                    SelectNote(0);

                }
                else
                {
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                if (obtainedNote[1] == true)
                {
                    SelectNote(1);

                }
                else
                {
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (obtainedNote[2] == true)
                {
                    SelectNote(2);

                }
                else
                {
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (obtainedNote[3] == true)
                {
                    SelectNote(3);

                }
                else
                {
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (obtainedNote[4] == true)
                {
                    SelectNote(4);

                }
                else
                {
                    return;
                }
            }
        }



        if (Input.GetMouseButton(0))
        {
            PlayNote();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopNote();
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < obtainedNote.Length; i++)
            {
                if (obtainedNote[i] == true)
                {
                    noteUIimage[i].color = noteColor[i];
                }
                else noteUIimage[i].color = Color.gray;
            }



            noteUI.SetActive(true);
            selectingNote = true;
        }
    }

    public void NoteAvailable(int num)
    {
        obtainedNote[num] = true;

    }

#region PlayNoteActions

    public void YesNote()
    {
        print("<color=red>Playing First Note</color>");

        if (obtainedNote[1] == false)
        {
            secNote.SoundTimer();
        }

    }

    public void LightNote()
    {
        print("<color=yellow>Playing Second Note</color>");
        if(light.on == true) { return; }
        light.LightTrigger(true);
    }

    public void BurstNote()
    {
        print("<color=green>Playing Third Note</color>");
        burst.Detonate();

    }

    public void NoNote()
    {
        print("<color=purple>Playing Fourth Note</color>");

        if(interactObject == "Mole Guard")
        {
            print("I shalt not hurt you");
            PuzzleManager.instance.guard.SpeakTo(currentNoteID);
        }
    }

    public void LastNote()
    {
        print("<color=blue>Playing Fifth Note</color>");
    }

    #endregion

    public void InteractableObject(string name)
    {
        interactObject = name;
    }


}
