using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlHouse : MonoBehaviour
{
    public GameObject owl;
    public GameObject lightEm;
    public GameObject nextNote;
    public Text recordText;
    public GameObject vinyl;
    public GameObject handle;
    public bool play;
    public bool recording;
    public string recorded;
    bool completed;
    float y;
    NoteManager note;

    private void Start()
    {
        recordText.text = "...";
        note = NoteManager.instance;
    }

    private void Update()
    {
        if(play == true)
        {
            vinyl.transform.Rotate(Vector3.up * Time.deltaTime * 10);
            handle.transform.Rotate(Vector3.back * Time.deltaTime * 30);
        }

        if(recording == true)
        {
            vinyl.transform.Rotate(-Vector3.up * Time.deltaTime * 10);
            handle.transform.Rotate(-Vector3.back * Time.deltaTime * 30);
        }
    }

    public void RecordButton(bool state)
    {
        if(state == true)
        {
            recordText.text = "Recording...";

        }
        else
        {
            recordText.text = "...";
        }
        recording = state;
        //GameManager.GM.recording = state;
        note.recording = state;
    }

    public void PlayButton()
    {
        recordText.text = "Playback...";
        print("Hit play");
        play = true;

        if (recorded != null)
        {

            print(recording);

            if(recorded == "Light")
            {
                LightState(true);

                if(completed == false)
                {
                    CompletedRecord();
                }
                
            }
            else
            {
                LightState(false);
            }
        }
    }
    public void StopButton()
    {
        play = false;
        recordText.text = "...";
        GameManager.GM.recording = false;
        LightState(false);

    }

    public void CompletedRecord()
    {
        completed = true;
        nextNote.SetActive(true);
        owl.GetComponent<OwlCharacter>().Completed();
    }

    public void LightState(bool l)
    {
        lightEm.SetActive(l);
    }

    public void AssignNote(int i)
    {
        if(i == 0)
        {
            recorded = "Yes";
        }
        if (i == 1)
        {
            recorded = "Light";

        }
        if (i == 2)
        {
            recorded = "Burst";

        }
        if (i == 3)
        {
            recorded = "No";

        }
        if (i == 4)
        {
            recorded = "Clear";

        }

        recordText.text = recorded;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PuzzleManager.instance.ChangePlayer(0);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //GameManager.GM.CameraChange(false);

        }
    }
}
