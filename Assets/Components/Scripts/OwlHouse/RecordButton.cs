using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{

    public OwlHouse house;
    Animator anim;

    private void Start()
    {
        //house = PuzzleManager.instance.owlHouse;
        anim = GetComponentInParent<Animator>();
    }

    public void ButtonPress(bool state)
    {
        anim.SetBool("Press", state);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            house.RecordButton(true);
            Fabric.EventManager.Instance.PostEvent("Misc/Pressureplate", gameObject);
            ButtonPress(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            house.RecordButton(false);
            ButtonPress(false);
        }

    }


}
