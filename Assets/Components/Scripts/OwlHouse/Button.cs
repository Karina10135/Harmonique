using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public OwlHouse house;

    public int buttonID;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        //house = PuzzleManager.instance.owlHouse;
    }

    public void ButtonPress(bool state)
    {
        anim.SetBool("Press", state);
    }

    public void ButtonAction(int ID)
    {
        if (ID == 0)
        {
            house.PlayButton();
        }
        if (ID == 1)
        {
            house.StopButton();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ButtonAction(buttonID);
            ButtonPress(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ButtonPress(false);

        }
    }
}
