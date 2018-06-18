﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is used for note management, whether a note is obtained.

public class NoteManager : MonoBehaviour
{

    public GameObject player;
    public int currentNoteID;
    public bool selectingNote;
    public ParticleSystem music;


    //UI vars
    public GameObject noteUI;
    public Image[] selectedNoteImages;
    public Image[] noteImage;

    public bool[] obtainedNote;
    public string interactObject;

    public Animator anim;

    public YesNote yes;
    public LightNote lightNote;
    public BurstNote burst;
    public NoNote no;
    public ClearNote clear;

    public Transform particlePosition;
    public ParticleSystem[] noteParticles;

    public GraveyardRiddle grave;
    public GatePuzzle gate;
    public MoleGuard guard;
    public MoleManager moles;
    public OwlHouse owlHouse;
    public bool moleSequence;
    public bool recording;
    public bool answeringMoleGuard;
    public bool gateRelay;



    public static NoteManager instance;

    private void Awake()
    {
        instance = this;
        obtainedNote = new bool[5];
        NoteAvailable(0);
        SelectNote(0);
    }

    void AnimSet(bool state)
    {
        if (anim == null) { return; }
        anim.SetBool("Playing", state);
        music.enableEmission = state;
    }

    private void Start()
    {
        instance = this;
        if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        yes = GetComponent<YesNote>();
        lightNote = GetComponent<LightNote>();
        burst = GetComponent<BurstNote>();
        no = GetComponent<NoNote>();
        clear = GetComponent<ClearNote>();
        player = GameManager.GM.player;
        anim = player.GetComponent<Animator>();


    }
    

    private void FixedUpdate()
    {
        ProcessInput();
    }

    public void SelectNote(int id)
    {

        if(obtainedNote[id] == false) { return; }
        currentNoteID = id;
        foreach(Image note in selectedNoteImages)
        {
            note.gameObject.SetActive(false);
        }
        selectedNoteImages[id].gameObject.SetActive(true);
        Destroy(music);
        music = Instantiate(noteParticles[id], particlePosition);
    }

    public void MoleHole()
    {
        if (!moleSequence) { return; }
    }

    public void PlayNote()
    {
        if (selectingNote) { return; }

        if(obtainedNote[0] == false) { return; }
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
        print(currentNoteID);
        music.Play();


        //SoundManager.instance.PlayNoteAudio(currentNoteID);

    }

    public void StopNote()
    {
        if (obtainedNote[0] == false) { return; }


        if(grave != null)
        {
            if (currentNoteID == 0 && grave.interacting == true)
            {
                grave.ResetTrigger();
                return;
            }
        }

        

        if(currentNoteID == 1)
        {
            lightNote.LightTrigger(false);
            return;
        }
    }

    public void ProcessInput()
    {
        if (selectingNote == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (obtainedNote[0] == true)
                {
                    SelectNote(0);
                    return;


                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                if (obtainedNote[1] == true)
                {
                    SelectNote(1);
                    return;


                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (obtainedNote[2] == true)
                {
                    burst.player = GameManager.GM.player;
                    print(burst.player);
                    print(GameManager.GM.player);
                    SelectNote(2);
                    return;


                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (obtainedNote[3] == true)
                {
                    SelectNote(3);
                    return;


                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (obtainedNote[4] == true)
                {
                    SelectNote(4);
                    return;


                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //If its interacting with one of the needed

            if(recording && owlHouse)
            {
                owlHouse.AssignNote(currentNoteID);

            }

            if (moleSequence)
            {
                moles.Check(currentNoteID);
            }

            if (answeringMoleGuard)
            {
                guard.SpeakTo(currentNoteID);
            }

            if (gateRelay)
            {
                gate.CheckNote(currentNoteID);
            }

            

        }

        //LEFT CLICK

        if (Input.GetMouseButtonDown(0))
        {
        }

        if (Input.GetMouseButton(0))
        {
            PlayNote();
            AnimSet(true);
            music.Play();

        }
        else
        {
            AnimSet(false);

            music.Stop();
        }


        if (Input.GetMouseButtonUp(0))
        {
            StopNote();
            music.Stop();

        }

        //RIGHT CLICK

        if (Input.GetMouseButton(1))
        {
            noteUI.SetActive(true);
            selectingNote = true;
        }
        else
        {
            noteUI.SetActive(false);
            selectingNote = false;
        }

    }

    public void NoteAvailable(int num)
    {
        obtainedNote[num] = true;
        noteImage[num].gameObject.SetActive(true);
    }

#region PlayNoteActions

    public void YesNote()
    {
        if(grave != null)
        {
            if (grave.interacting == true)
            {
                grave.StartTrigger();
            }
        }
        

    }

    public void LightNote()
    {
        if(lightNote.on == true) { return; }
        lightNote.LightTrigger(true);
    }

    public void BurstNote()
    {
        burst.Detonate();

    }

    public void NoNote()
    {
        no.SayNo();

    }

    public void LastNote()
    {
    }

    #endregion

    public void InteractableObject(string name)
    {
        interactObject = name;
    }


}
