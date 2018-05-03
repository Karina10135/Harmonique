using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInputManager : MonoBehaviour
{
    public int selectedNoteID;

    public GameObject noteUI;
    public Image selectedNoteImage;
    public Image[] noteUIimage;
    public Color[] noteColor;

    bool selectingNote;
    public bool[] noteAvailable;

    public void Start()
    {
        

        //foreach(Image note in noteUIimage)
        //{
        //    for (int i = 0; i < noteUIimage.Length; i++)
        //    {
        //        note.color = noteColor[i];
        //    }
        //}

        

        for (int i = 0; i < noteUIimage.Length; i++)
        {
            noteUIimage[i].color = noteColor[i];
        }
    }

    public void Update()
    {
        ProcessInput();

    }

    public void ProcessInput()
    {
        if(selectingNote == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                if(noteAvailable[0] == true)
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

                if (noteAvailable[1] == true)
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
                if (noteAvailable[2] == true)
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
                if (noteAvailable[3] == true)
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
                if (noteAvailable[4] == true)
                {
                    SelectNote(4);

                }
                else
                {
                    return;
                }
            }
        }
        


        if (Input.GetMouseButtonDown(0))
        {
            PlayNote();
        }

        if (Input.GetMouseButtonDown(1))
        {
            for(int i = 0; i < noteAvailable.Length; i++)
            {
                if (noteAvailable[i] == true)
                {
                    noteUIimage[i].color = noteColor[i];
                }
                else noteUIimage[i].color = Color.gray;
            }

            

            noteUI.SetActive(true);
            selectingNote = true;
        }
    }

    public void SelectNote(int num)
    {
        selectedNoteID = num;
        selectedNoteImage.color = noteColor[num];

        //foreach (Image note in noteUIimage)
        //{
        //    note.color = Color.black;
        //}

        noteUIimage[num].color = noteColor[num];
        noteUI.SetActive(false);
        selectingNote = false;
    }

    #region NoteProcessor

    public void PlayNote()
    {
        if (selectedNoteID == 0)
        {
            YesNote();
        }

        if (selectedNoteID == 1)
        {
            LightNote();
        }

        if (selectedNoteID == 2)
        {
            BurstNote();
        }

        if (selectedNoteID == 3)
        {
            NoNote();
        }

        if (selectedNoteID == 4)
        {
            LastNote();
        }
    }

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

    #endregion;

}
