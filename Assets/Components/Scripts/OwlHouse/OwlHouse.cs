using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlHouse : MonoBehaviour
{
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

    private void Start()
    {
        PuzzleManager.instance.owlHouse = this;
        NoteManager.instance.owlHouse = this;
        play = true;
        recordText.text = "...";
    }

    private void Update()
    {
        if(play == true)
        {
            vinyl.transform.Rotate(Vector3.up * Time.deltaTime * 10);
            handle.transform.Rotate(Vector3.back * Time.deltaTime * 30);
            //y += Time.deltaTime * 10;
            //vinyl.transform.rotation = vinyl.transform.rotation()
            //    Quaternion.Euler(0, y, 0);
            //vinly.transform.rotation = vinyl.transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
        }
    }

    public void RecordButton(bool state)
    {
        //if (playback) { return; }

        recordText.text = "Recording...";
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
        recordText.text = "Playback...";
        GameManager.GM.recording = false;
        print("Hit play");

        if (recorded != null)
        {

            print(recording);

            if(recorded == "Light")
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
        recordText.text = "...";
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
