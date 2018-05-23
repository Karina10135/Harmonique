using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    OwlHouse house;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ButtonPress(bool state)
    {
        anim.SetBool("Press", state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            house.PlayButton();
            ButtonPress(true);

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ButtonPress(false);
        }
    }




}
