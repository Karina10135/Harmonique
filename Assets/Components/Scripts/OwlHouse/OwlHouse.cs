using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlHouse : MonoBehaviour
{
    public GameObject lightEm;
    public GameObject nextNote;

    public string recording;
    bool completed;

    private void Start()
    {
        PuzzleManager.instance.owlHouse = this;
        NoteManager.instance.owlHouse = this;

    }

    public void RecordButton(bool state)
    {
        //if (playback) { return; }

        GameManager.GM.recording = state;

        //if(state == true)
        //{
        //    GameManager.GM.recording = true;
        //    print("recording");


        //}
        //else
        //{
        //    GameManager.GM.recording = false;
        //    print("stopped recording");

        //}

    }

    public void PlayButton()
    {
        GameManager.GM.recording = false;
        print("Hit play");

        if (recording != null)
        {

            print(recording);

            if(recording == "Light")
            {
                LightState(true);

                if(completed == false)
                {
                    completed = true;
                    nextNote.SetActive(true);
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
        print("hit stop");
        GameManager.GM.recording = false;
        LightState(false);

        //if (playingLight == true)
        //{
        //    LightState(false);
        //}
    }

    public void LightState(bool l)
    {
        lightEm.SetActive(l);
    }

    public void AssignNote(int i)
    {
        if(i == 0)
        {
            recording = "Yes";
        }
        if (i == 1)
        {
            recording = "Light";

        }
        if (i == 2)
        {
            recording = "Burst";

        }
        if (i == 3)
        {
            recording = "No";

        }
        if (i == 4)
        {
            recording = "Clear";

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GM.SceneChange("Main");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //GameManager.GM.CameraChange(false);
            print("player exit");

        }
    }
}
