using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    public ParticleSystem[] dirtVFX;
    public GameObject noteObj;
    public float timer;
    public float currentTime;

    public int[] moleID; //Mole number.

    public int currentMole; //Will track the current mole.
    public int currentMoleSequence; //Will track which play sequence its on.
    public int moleSeq;
    public int currentNote; //Which note must be played.
    

    public Animator moleAnimator;
    public Transform[] moleTransforms;

    bool[] IDtaken;
    bool timing;
    bool complete;

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
        //if(timing == true)
        //{
        //    Timer();

            
        //}

    }

    public void Check(int noteID)
    {
        if(noteID == currentNote)
        {
            moleAnimator.SetTrigger("Correct");



            //if (moleSeq != currentMole)
            //{
            //    moleSeq++;
            //    PlayMole();
            //    return;
            //}


            //if(currentMole == 3)
            //{
            //    CompletedPuzzle();
            //}
            //else
            //{
            //    NextNote();
            //}
        }
        else
        {
            moleAnimator.SetTrigger("Incorrect");
            
            StartPuzzle();
        }
    }

    public void StartPuzzle()
    {
        currentMoleSequence = 0;
        moleSeq = 0;
        currentMole = 0;
        IDtaken[0] = false;
        IDtaken[1] = false;
        IDtaken[2] = false;
        IDtaken[3] = false;

        ExitPuzzle();
        StartCoroutine(StartTime());

    }

    public void ExitPuzzle()
    {
        timing = false;
        note.moleSequence = false;
        moleAnimator.SetBool("Started", false);

    }

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

        PlayMole();
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
        currentNote = moleID[moleSeq];
        
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
        moleSeq = 0;
        dirtVFX[currentMole].Play();
        currentMole++;
        PlayMole();
    }


    public void CompletedPuzzle()
    {
        if(complete == true) { return; }

        complete = true;
        noteObj.SetActive(true);
        note.moleSequence = false;

    }


    #region TimerFunctions

    public void ResetTime()
    {
        currentTime = timer;
        timing = true;

    }

    public void Timer()
    {
        currentTime -= Time.deltaTime;

        if(currentTime < 0)
        {
            StartPuzzle();
        }
    }

    IEnumerator StartTime() //Timing interactable with puzzle.
    {
        timing = false;
        yield return new WaitForSeconds(2);
        note.moleSequence = true;
        ResetTime();
        Assign();
    }

    IEnumerator MolePlayTimer()
    {
        //Play current mole note.

        yield return new WaitForSeconds(1);
        //Next job (play next mole or wait for recall)
    }

    #endregion

    private void OnTriggerEnter(Collider other) //Exit mole cave.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PuzzleManager.instance.ChangePlayer(0);
            ExitPuzzle();
        }
    }

   

}
