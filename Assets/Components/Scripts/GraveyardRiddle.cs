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
    bool timing;
    bool completed;
    public bool interacting;

    Animator anim;

    private void Start()
    {
        ResetTrigger();
    }

    private void Update()
    {
        if (timing)
        {
            SoundTimer();
        }
    }

    public void StartTrigger()
    {
        Fabric.EventManager.Instance.PostEvent("Tomb/Tune", Fabric.EventAction.PlaySound, headstoneTrumpets);
        print("Triggered grave trumpets");
        timing = true;

    }

    public void SoundTimer()
    {

        if (completed) { return; }


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
        timing = false;
        Fabric.EventManager.Instance.PostEvent("Tomb/Tune", Fabric.EventAction.StopSound, headstoneTrumpets);

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
