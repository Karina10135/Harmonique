using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlHouse : MonoBehaviour
{
    public GameObject lightEm;
    public GameObject nextNote;

    public string recording;
    bool playback;
    bool playingLight;
    bool completed;

    public void RecordButton()
    {
        if (playback) { return; }

        GameManager.GM.recording = true;


        print("recording");
    }

    public void PlayButton()
    {
        GameManager.GM.recording = false;
        print("Hit play");

        if (recording != null)
        {
            playback = true;

            print(recording);

            if(recording == "Light")
            {
                LightState(true);
                playingLight = true;

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
        playback = false;
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
            GameManager.GM.CameraChange(true);
            print("player entered");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GM.CameraChange(false);
            print("player exit");

        }
    }
}
