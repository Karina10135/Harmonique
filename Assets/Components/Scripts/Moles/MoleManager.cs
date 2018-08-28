using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject[] bubbles;
    public ParticleSystem[] dirtVFX;
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
    bool timing;
    bool complete;

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
        if (complete) { return; }
        

    }

    public void EnterMoleCave()
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

        moleAnimator.SetBool("Started", true);

        PlayMoleSequence();
    }

    //Compare note played.
    public void Check(int noteID)
    {
        if (speak)
        {
            if(num != 1)
            {
                bubbles[1].SetActive(true);
                num = 1;
                return;
            }
            
            StartPuzzle();
            return;
        }
        
        if(noteID == currentNote)
        {
            if(currentMole == currentMoleSequence)
            {
                PlayMoleSequence(); //go to the next sequence
            }
            else
            {
                NextNote();
            }

        }
        else
        {
            moleAnimator.SetTrigger("Incorrect");
            StartPuzzle();
        }
    }

    

    public void ExitPuzzle()
    {
        timing = false;
        note.moleSequence = false;
        moleAnimator.SetBool("Started", false);
        speechBubble.SetActive(false);
    }

    

    public void PopUpMole(int id)
    {


        int note = Random.RandomRange(0, 4);
        if(IDtaken[note] == false)
        {
            moleID[id] = note;
            IDtaken[note] = true;
        }
        else
        {
            PopUpMole(id);
            return;
        }

        //ResetTime();


    }

    public void PlayMole()
    {
        currentNote = moleID[currentMoleSequence];
        
        int id = currentNote + 1;
        string mole = "Mole/" + id.ToString();
        Fabric.EventManager.Instance.PostEvent(mole, moleTransforms[currentMole].gameObject);
    }

    public void PlayMoleSequence()
    {
        currentNote = moleID[currentMoleSequence];
        for(int i = 0; i < currentMoleSequence; i++)
        {
            dirtVFX[i].Play();
        }

        
    }

    public void NextNote()
    {
        currentMole++;
        currentNote = moleID[currentMole];
    }


    public void CompletedPuzzle()
    {
        if(complete == true) { return; }

        complete = true;
        noteObj.SetActive(true);
        speechBubble.SetActive(true);
        bubbles[2].SetActive(true);
        note.moleSequence = false;

    }


    

    

    //IEnumerator StartTime() //Timing interactable with puzzle.
    //{
    //    timing = false;
    //    yield return new WaitForSeconds(2);
    //    note.moleSequence = true;
    //    Assign();
    //}

    IEnumerator StartTime(float time)
    {
        yield return new WaitForSeconds(time);
        MoleSpeak();
        moleAnimator.SetBool("Started", true);
        yield return new WaitForSeconds(1);
        speechBubble.SetActive(true);
    }

    IEnumerator MolePlayTimer()
    {
        //Play current mole note.

        yield return new WaitForSeconds(1);
        //Next job (play next mole or wait for recall)
    }


    private void OnTriggerEnter(Collider other) //Exit mole cave.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PuzzleManager.instance.ChangePlayer(0);
            ExitPuzzle();
        }
    }

   

}
