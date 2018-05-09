using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNote : MonoBehaviour
{
    public GameObject note;
    public float timeInterval;
    public float currentTime;
    public int state;
    int currentState;
    bool completed;

    private void Start()
    {
        currentTime = timeInterval;
    }

    public void SoundTimer()
    {

        if (completed) { return; }

        if (currentState == state)
        {
            note.SetActive(true);
            completed = true;
            return;
        }

        if (currentTime < 0)
        {
            currentState++;
            print("Next state" + currentState + "/" + state);
            currentTime = timeInterval;
        }
        else
        {
            currentTime -= Time.deltaTime;
            print("<color=yellow> TIMING </color>");

        }




    }

    public void StateTrigger(int state)
    {
        //The triggers for state that has to be reset on state reset.
    }


    public void ResetTrigger()
    {
        currentState = 0;
        currentTime = timeInterval;
    }
}
