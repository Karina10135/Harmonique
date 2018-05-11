using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    public Text moleNote;
    public GameObject noteObj;
    public float timer;
    public float currentTime;
    public Image[] moleCharacter;
    public int[] moleID;

    public int currentMole;
    public int moleSeq;
    public int currentNote;
    

    public Color[] noteCol;

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
        //StartPuzzle();

        Assign();
    }

    private void Update()
    {
        if(timing == true)
        {
            Timer();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Check();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Check();
        }
    }

    public void Check()
    {
        if(NoteManager.instance.currentNoteID == currentNote)
        {
            if(moleSeq != currentMole)
            {
                moleSeq++;
                PlayMole(moleSeq);
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
            Assign();
        }
    }

    public void StartPuzzle()
    {
        IDtaken[0] = false;
        IDtaken[1] = false;
        IDtaken[2] = false;
        IDtaken[3] = false;
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
        PlayMole(0);
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

    public void PlayMole(int id)
    {
        currentNote = moleID[moleSeq];
        moleNote.text = currentNote.ToString();

    }

    public void NextNote()
    {
        moleSeq = 0;
        currentMole++;
        PlayMole(currentMole);
    }


    public void CompletedPuzzle()
    {
        if(complete == true) { return; }

        complete = true;
        noteObj.SetActive(true);
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

    IEnumerator StartTime()
    {
        timing = false;
        yield return new WaitForSeconds(2);
        ResetTime();
        PopUpMole(0);
    }

#endregion



}
