using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is used for note management, whether a note is obtained.

public class NoteManager : MonoBehaviour
{

    public GameObject player;
    public int currentNoteID;
    public bool selectingNote;


    //UI vars
    public GameObject noteUI;
    public Image[] noteImages;
    public Image selectedNote;

    [HideInInspector]
    public bool[] obtainedNote;
    [HideInInspector]
    public string interactObject;

    public Animator anim;
    public ParticleSystem yesParticle;
    public ParticleSystem noParticle;


    public YesNote yes;
    public LightNote lightNote;
    public BurstNote burst;
    public NoNote no;
    public ClearNote clear;

    public GraveyardRiddle grave;
    public GatePuzzle gate;
    public MoleGuard guard;
    public MoleManager moles;
    public OwlHouse owlHouse;
    public bool moleSequence;
    public bool recording;
    public bool answeringMoleGuard;
    public bool gateRelay;

    public bool playingMusic;
    Animator noteAnim;
    string audioNote;

    public CameraMoveToPoint cam;
    public static NoteManager instance;
    GameManager gm;

    private void Awake()
    {
        //gm = GameManager.GM;
        //gm.FadingInToScene();

        instance = this;

        if(instance != this) { Destroy(this); }
        obtainedNote = new bool[5];
        NoteAvailable(0);
        SelectNote(0);
    }

    

    private void Start()
    {
        instance = this;
        if(instance != this)
        {
            Destroy(gameObject);
        }

        yesParticle.Stop();
        noParticle.Stop();
        yes = GetComponent<YesNote>();
        lightNote = GetComponent<LightNote>();
        burst = GetComponent<BurstNote>();
        no = GetComponent<NoNote>();
        clear = GetComponent<ClearNote>();
        burst.player = player;
        //player = GameManager.GM.player;
        //anim = player.GetComponent<Animator>();
        noteAnim = noteUI.GetComponent<Animator>();
        audioNote = "Note/1";

    }
    

    private void FixedUpdate()
    {
        ProcessInput();
        ProcessAnimation();
    }

    public void SelectNote(int id)
    {

        if(obtainedNote[id] == false) { return; }

        currentNoteID = id;
        selectedNote.sprite = noteImages[id].sprite;

        id++;
        audioNote = "Note/" + id.ToString();

        return;
    }

    public void MoleHole()
    {
        if (!moleSequence) { return; }
    }

    void ProcessAnimation()
    {
        AnimSet(playingMusic);

    }

    void AnimSet(bool state)
    {
        anim.SetBool("Playing", state);

    }



    

    public void ProcessInput()
    {
        if (cam.isPaused) { return; }

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



        if (playingMusic)
        {
            PlayNote();
        }

        

        //RIGHT CLICK

        if (Input.GetMouseButton(1))
        {
            noteAnim.SetBool("Selecting", true);
            selectingNote = true;
        }
        else
        {
            selectingNote = false;
            noteAnim.SetBool("Selecting", false);

        }

        if (Input.GetMouseButtonUp(1))
        {
            selectingNote = false;
            noteAnim.SetBool("Selecting", false);

        }

    }

    public void NoteAvailable(int num)
    {
        obtainedNote[num] = true;
        noteImages[num].gameObject.SetActive(true);
        
    }

    #region PlayNoteActions

    public void PlayNote()
    {
        if (selectingNote) { return; }
        if (!playingMusic) { return; }


        if (obtainedNote[0] == false) { return; }
        if (currentNoteID == 0)
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


        //SoundManager.instance.PlayNoteAudio(currentNoteID);

    }

    public void PlayTrigger()
    {
        playingMusic = true;

        if(currentNoteID == 0)
        {
            yesParticle.Play();

            if (grave != null)
            {
                if (grave.interacting == true)
                {
                    grave.StartTrigger();
                }
            }
        }

        if(currentNoteID == 2)
        {
            burst.ForceParticle(true);
        }

        if(currentNoteID == 3)
        {
            noParticle.Play();
        }



        if (recording && owlHouse)
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

        Fabric.EventManager.Instance.PostEvent(audioNote, Camera.main.gameObject);

        //Fabric.EventManager.Instance.PostEvent("Note/1", Camera.main.gameObject);

    }


    public void YesNote()
    {


        if(grave != null)
        {
            if (grave.interacting == true)
            {
                //grave.StartTrigger();
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

    public void StopNote()
    {
        if (obtainedNote[0] == false) { return; }
        playingMusic = false;
        Fabric.EventManager.Instance.PostEvent(audioNote, Fabric.EventAction.StopSound, Camera.main.gameObject);
        yesParticle.Stop();
        noParticle.Stop();

        if (grave != null)
        {
            if (currentNoteID == 0 && grave.interacting == true)
            {
                grave.ResetTrigger();
                return;
            }
        }


        if (currentNoteID == 0)
        {
            yesParticle.Stop();
            return;
        }

        if (currentNoteID == 3)
        {
            noParticle.Stop();
            return;
        }


        if (currentNoteID == 1)
        {
            lightNote.LightTrigger(false);
            return;
        }

        if(currentNoteID == 2)
        {
            burst.ForceParticle(false);
        }


    }

    public void InteractableObject(string name)
    {
        interactObject = name;
    }


}
