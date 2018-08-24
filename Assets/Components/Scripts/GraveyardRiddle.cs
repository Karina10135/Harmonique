using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardRiddle : MonoBehaviour
{
    public GameObject note;
    public GameObject lightBeam;
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
        if (completed) { return; }
        Fabric.EventManager.Instance.PostEvent("Tomb/Tune", Fabric.EventAction.PlaySound, headstoneTrumpets);
        timing = true;

    }

    public void SoundTimer()
    {

        if (completed) { return; }


        if (currentState == state)
        {
            note.SetActive(true);
            lightBeam.SetActive(true);
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
        if (completed) { return; }

        timing = false;

        Fabric.EventManager.Instance.PostEvent("Tomb/Tune", Fabric.EventAction.StopSound, headstoneTrumpets);

        currentState = 0;
        currentTime = timeInterval;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (completed) { return; }
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
