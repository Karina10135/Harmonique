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
    public Image selectedNoteImage;
    public Image[] noteImage;

    public bool[] obtainedNote;
    public string interactObject;

    public Animator anim;
    bool playing;


    public YesNote yes;
    public LightNote lightNote;
    public BurstNote burst;
    public NoNote no;
    public ClearNote clear;

    public Transform particlePosition;
    public ParticleSystem[] noteParticles;

    [HideInInspector]
    public GraveyardRiddle grave;
    [HideInInspector]
    public GatePuzzle gate;
    [HideInInspector]
    public MoleGuard guard;
    [HideInInspector]
    public MoleManager moles;
    [HideInInspector]
    public OwlHouse owlHouse;
    [HideInInspector]
    public bool moleSequence;
    //bool recording;



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



        lightNote.lightObject = GetComponentInChildren<Light>();

        if(anim == null)
        {
            print("No Animator");
        }

    }
    

    private void FixedUpdate()
    {
        ProcessInput();
    }

    public void SelectNote(int id)
    {

        if(obtainedNote[id] == false) { return; }
        currentNoteID = id;
        Image i = selectedNoteImage.GetComponent<Image>();
        i = noteImage[id];
        print("selected note");
        Destroy(music);
        music = Instantiate(noteParticles[id], particlePosition);
    }

    public void MoleHole()
    {
        if (!moleSequence) { return; }
    }

    public void PlayNote()
    {
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
        playing = true;
        music.Play();


        //SoundManager.instance.PlayNoteAudio(currentNoteID);

    }

    public void StopNote()
    {
        if (obtainedNote[0] == false) { return; }

        //SoundManager.instance.StopAudio();
        playing = false;

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
            

            if(owlHouse != null)
            {
                if (GameManager.GM.recording == true)
                {
                    owlHouse.AssignNote(currentNoteID);
                }
            }
            
            if(moles != null)
            {
                if (moleSequence == true)
                {
                    moles.Check(currentNoteID);
                }
            }
            

            if(guard != null)
            {
                if (guard.answering == true)
                {
                    guard.SpeakTo(currentNoteID);
                }
            }

            if(gate != null)
            {
                if (PuzzleManager.instance.gate.playing == true)
                {
                    PuzzleManager.instance.gate.CheckNote(currentNoteID);
                }
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
            playing = false;
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
    }

#region PlayNoteActions

    public void YesNote()
    {
        print("<color=red>Playing First Note</color>");
        if(grave != null)
        {
            if (grave.interacting == true)
            {
                grave.SoundTimer();
            }
        }
        

    }

    public void LightNote()
    {
        print("<color=yellow>Playing Second Note</color>");
        if(lightNote.on == true) { return; }
        lightNote.LightTrigger(true);
    }

    public void BurstNote()
    {
        print("<color=green>Playing Third Note</color>");
        burst.Detonate();

    }

    public void NoNote()
    {
        print("<color=purple>Playing Fourth Note</color>");
        no.SayNo();

        //if(interactObject == "Mole Guard")
        //{
        //    print("I shalt not hurt you");
        //    PuzzleManager.instance.guard.SpeakTo(currentNoteID);
        //}
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
