using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNote : MonoBehaviour
{

    public float timeInterval;
    public float currentTime;
    public int state;
    int currentState;

    private void Start()
    {
        currentTime = timeInterval;
    }

    public void SoundTimer()
    {

        if (NoteManager.instance.obtainedNote[1] == true) { return; }

        if (currentState == state)
        {
            NoteManager.instance.obtainedNote[1] = true;
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
