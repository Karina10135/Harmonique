using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardRiddle : MonoBehaviour
{
    public GameObject note;
    public GameObject headstoneTrumpets;


    public float timeInterval;
    public float currentTime;
    public int state;

    int currentState;
    bool completed;
    public bool interacting;


    Animator anim;

    public void SoundTimer()
    {

        if (completed) { return; }

        if (currentState == state)
        {
            note.SetActive(true);
            Animator anim = headstoneTrumpets.GetComponent<Animator>();
            anim.SetBool("Playing", true);
            headstoneTrumpets.GetComponentInChildren<ParticleSystem>().Play();
            completed = true;
            return;
        }

        if (currentTime < 0)
        {
            currentState++;
            print("Next state" + currentState + "/" + state);
            currentTime = timeInterval;
            StateTrigger(currentState);
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
        //anim = headstone[state].GetComponent<Animator>();
        //anim.SetBool("", true);

    }

    public void ResetTrigger()
    {
        currentState = 0;
        currentTime = timeInterval;
        //for(int i = 0; i < headstone.Length; i++)
        //{
        //    anim = headstone[i].GetComponent<Animator>();
        //    anim.SetBool("", false);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interacting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interacting = false;
            ResetTrigger();
        }
    }

}
