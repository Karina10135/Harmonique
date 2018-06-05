using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{

    public float offset;
    public GameObject noteObj;
    public float timer;
    public float currentTime;

    public int[] moleID;

    public int currentMole;
    public int moleSeq;
    public int currentNote;
    

    public Color[] noteCol;

    public Animator moleAnimator;
    public Transform[] moleTransforms;
    public ParticleSystem[] notesVFX;

    public string takenNote;
    public bool[] IDtaken;
    bool timing;
    bool complete;

    NoteManager note;

    private void Start()
    {
        note = NoteManager.instance;

        moleID = new int[4];
        IDtaken = new bool[4];
        //ResetPos();

    }

    public void ResetPos()
    {
        
    }

    private void Update()
    {

        if (complete) { return; }
        if(timing == true)
        {
            Timer();

            
        }

    }

    public void Check(int noteID)
    {
        if(noteID == currentNote)
        {
            moleAnimator.SetTrigger("Correct");

            if (moleSeq != currentMole)
            {
                moleSeq++;
                PlayMole();
                return;
            }


            if(currentMole == 3)
            {
                CompletedPuzzle();
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

    public void StartPuzzle()
    {
        print("Start Trigger");
        IDtaken[0] = false;
        IDtaken[1] = false;
        IDtaken[2] = false;
        IDtaken[3] = false;
        note.moleSequence = true;

        StartCoroutine(StartTime());

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
        if(notesVFX.Length > 0)
        {
            Instantiate(notesVFX[currentNote], moleTransforms[currentMole]);
        }
        ResetTime();
    }

    public void NextNote()
    {
        moleSeq = 0;
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
        print("Timing");
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

    IEnumerator StartTime()
    {
        timing = false;
        yield return new WaitForSeconds(2);
        ResetTime();
        Assign();
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PuzzleManager.instance.ChangePlayer(0);
            note.moleSequence = false;
        }
    }

}
