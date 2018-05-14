using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{

    public float offset;
    public Text moleNote;
    public Text timeText;
    public GameObject noteObj;
    public float timer;
    public float currentTime;



    public GameObject[] moleCharacter;
    public Transform[] orgPos;
    public Transform[] popUpPos;

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
        orgPos = new Transform[4];
        popUpPos = new Transform[4];


    }

    public void ResetPos()
    {
        for(int i = 0; i< moleCharacter.Length; i++)
        {
            //orgPos[i] = moleCharacter[i].transform;
            Vector3 pos = new Vector3(moleCharacter[i].transform.position.x, moleCharacter[i].transform.position.y + 1, moleCharacter[i].transform.position.z);
            popUpPos[i].transform.position = pos;
        }
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
            //MoleTransform(moleSeq, false);
            //moleCharacter[moleSeq].transform.position = orgPos[moleSeq].position;

            if (moleSeq != currentMole)
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
            //moleCharacter[moleSeq].transform.position = orgPos[moleSeq].position;
            //MoleTransform(currentMole, false);
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
        NoteManager.instance.moleSequence = true;

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
        NoteManager.instance.moleSequence = true;
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

    public void MoleTransform(int i, bool play)
    {

        if(play == true)
        {
            Vector3 pos = new Vector3(moleCharacter[i].transform.position.x, moleCharacter[i].transform.position.y + offset, moleCharacter[i].transform.position.z);
            moleCharacter[i].transform.position = pos;
        }
        else
        {
            Vector3 pos = new Vector3(moleCharacter[i].transform.position.x, moleCharacter[i].transform.position.y - offset, moleCharacter[i].transform.position.z);
            moleCharacter[i].transform.position = pos;
        }
        

    }

    public void PlayMole(int id)
    {
        currentNote = moleID[moleSeq];
        moleNote.text = currentNote.ToString();
        //moleCharacter[id].transform.position = popUpPos[id].position;
        //MoleTransform(moleSeq, true);
        ResetTime();
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
        NoteManager.instance.moleSequence = false;

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
        timeText.text = currentTime.ToString("00");
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



}
