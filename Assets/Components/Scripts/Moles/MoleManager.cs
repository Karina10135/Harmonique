using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject[] bubbles;
    public GameObject yourTurnBubble;
    public GameObject incorrectBubble;
    public GameObject completeBubble;

    public ParticleSystem[] dirtVFX;
    public ParticleSystem[] moleParticle;
    public Material[] materialColor;

    public GameObject noteObj;
    public float timer;
    public float currentTime;

    public int[] moleID; //Mole number.
    public int currentMole; //Will track the current mole.
    public int currentMoleSequence; //Will track which play sequence its on.
    public int currentNote; //Which note must be played.
    

    public Animator moleAnimator;
    public Transform[] moleTransforms;

    bool[] IDtaken;
    bool complete;
    bool respond;

    int num;
    bool speak;
    NoteManager note;

    private void Start()
    {
        note = NoteManager.instance;
        
        moleID = new int[4];
        IDtaken = new bool[4];

    }

    private void Update()
    {
        
        

    }

    public void EnterMoleCave() //Triggered when entering cave.
    {
        if (complete) { return; }
        StartCoroutine(StartTime(3));
    }

    public void MoleSpeak()
    {
        speak = true;
        num = 0;
        foreach(GameObject bubble in bubbles)
        {
            bubble.SetActive(false);
        }

        bubbles[0].SetActive(true);
    }



    //Start puzzle.
    public void StartPuzzle()
    {
        speak = false;
        currentMoleSequence = 0;
        currentMole = 0;
        IDtaken[0] = false;
        IDtaken[1] = false;
        IDtaken[2] = false;
        IDtaken[3] = false;

        ExitPuzzle();

        Assign();

    }

    //Assign notes to moles.
    public void Assign() 
    {
        IDtaken[0] = false;
        IDtaken[1] = false;
        IDtaken[2] = false;
        IDtaken[3] = false;
        for (int i = 0; i < moleID.Length; i++)
        {
            PopUpMole(i);

        }
        note.moleSequence = true;

        //moleAnimator.SetBool("Started", true);
        StartCoroutine(MolePlayTimer());
    }

    //Compare note played.
    public void Check(int noteID)
    {
        if (complete) { return; }

        if (speak)
        {
            if(num != 2)
            {
                num++;
                bubbles[num].SetActive(true);
                return;
            }

            speechBubble.SetActive(false);
            StartPuzzle();
            return;
        }

        if (!respond) { return; }

        if(noteID == currentNote)
        {

            if (currentNote == moleID[currentMoleSequence] )
            {
                yourTurnBubble.SetActive(false);

                if (currentMoleSequence == 3)
                {
                    CompletedPuzzle();
                    return;
                }
                respond = false;
                currentMoleSequence++;
                currentMole = 0;
                moleAnimator.SetTrigger("Correct");
                StartCoroutine(MolePlayTimer()); //go to the next sequence
            }
            else
            {
                NextNote();
            }

        }
        else
        {
            yourTurnBubble.SetActive(false);

            moleAnimator.SetTrigger("Reset");
            respond = false;
            StartCoroutine(ResetTimer());
            
        }
    }

    

    

    

    public void PopUpMole(int id) //Gives each mole a note ID.
    {


        int note = Random.RandomRange(0, 4);
        if(IDtaken[note] == false)
        {
            moleID[id] = note;
            IDtaken[note] = true;
            moleParticle[id].GetComponent<ParticleSystemRenderer>().material = materialColor[note];
        }
        else
        {
            PopUpMole(id);
            return;
        }


    }


    public void PlayMoleSequence()
    {
        //all moles in sequence pop up.

        currentNote = moleID[currentMole];

        int id = currentNote + 1;
        string mole = "Mole/" + id.ToString();
        print(mole);
        Fabric.EventManager.Instance.PostEvent(mole, moleTransforms[currentMole].gameObject);
        moleParticle[currentMole].Play();

        if (currentMole == currentMoleSequence)
        {
            currentMole = 0;
            currentNote = moleID[currentMole];
            StartCoroutine(WaitTimer());

            return;
        }

        currentMole++;
        StartCoroutine(MolePlayTimer());



    }

    public void NextNote() //Read for next note
    {
        currentMole++;
        currentNote = moleID[currentMole];
    }


    public void CompletedPuzzle() //Complete function
    {
        if(complete == true) { return; }

        complete = true;
        noteObj.SetActive(true);
        completeBubble.SetActive(true);
        bubbles[2].SetActive(true);
        note.moleSequence = false;

    }

    public void ExitPuzzle()
    {
        note.moleSequence = false;
        moleAnimator.SetTrigger("Reset");
        speechBubble.SetActive(false);
        incorrectBubble.SetActive(false);
        yourTurnBubble.SetActive(false);
    }

    IEnumerator ResetTimer()
    {
        moleAnimator.SetTrigger("Start");
        dirtVFX[0].Play();
        yield return new WaitForSeconds(2);
        incorrectBubble.SetActive(true);
        yield return new WaitForSeconds(2);
        moleAnimator.SetTrigger("Reset");
        incorrectBubble.SetActive(false);
        StartPuzzle();
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(2);
        respond = true;
        yourTurnBubble.SetActive(true);


    }

    IEnumerator StartTime(float time)
    {
        yield return new WaitForSeconds(time);
        MoleSpeak();
        moleAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        speechBubble.SetActive(true);
    }

    IEnumerator MolePlayTimer()
    {
        //Play current mole note.
        if (currentMole == 0)
        {
            moleAnimator.SetTrigger("Start");

            for (int i = 0; i < currentMoleSequence; i++)
            {
                dirtVFX[i].Play();
            }
        }
        yield return new WaitForSeconds(2);
        PlayMoleSequence();
        //Next job (play next mole or wait for recall)
    }


    private void OnTriggerEnter(Collider other) //Exit mole cave.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PuzzleManager.instance.ChangePlayer(0);
            ExitPuzzle();
            GameManager.GM.TurnMixerDown(false);
        }
    }

   

}
