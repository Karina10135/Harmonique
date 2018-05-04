using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSecondNote : MonoBehaviour
{
    public float timeInterval;
    public float currentTime;
    public int state;
    int currentState;

    public static TriggerSecondNote instance;

    private void Start()
    {
        instance = this;
        currentTime = timeInterval;
    }

    public void SoundTimer()
    {

        if (NoteManager.instance.obtainedNote[1] == true) { return; }

        if(currentState == state)
        {
            NoteManager.instance.NoteAvailable(1);
            return;
        }
        
        if(currentTime < 0)
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

    public void StateTrigger(int stat)
    {
        //The triggers for state that has to be reset on state reset.
    }


    public void ResetTrigger()
    {
        currentState = 0;
        currentTime = timeInterval;
    }

}
