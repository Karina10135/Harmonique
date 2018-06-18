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

    private void Start()
    {
        ResetTrigger();
    }

    public void SoundTimer()
    {

        if (completed) { return; }

        Fabric.EventManager.Instance.PostEvent("Tomb/Tune");

        if (currentState == state)
        {
            note.SetActive(true);
            Animator anim = headstoneTrumpets.GetComponent<Animator>();
            anim.SetBool("Playing", true);
            completed = true;
            return;
        }

        if (currentTime < 0)
        {
            currentState++;
            currentTime = timeInterval;
        }
        else
        {
            currentTime -= Time.deltaTime;

        }






    }

    

    public void ResetTrigger()
    {
        currentState = 0;
        currentTime = timeInterval;
        
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
