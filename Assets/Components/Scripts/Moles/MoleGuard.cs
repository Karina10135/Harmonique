﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGuard : MonoBehaviour
{
    public GameObject speechBox;
    public GameObject[] bubbles;


    public float maxRadius;
    public Transform centre;
    public GameObject target;
    public bool answering;


    private void Start()
    {
        
        PuzzleManager.instance.guard = this;
        NoteManager.instance.guard = this;

    }
    private void Update()
    {
        CheckCollision();
    }

    public void CheckCollision()
    {
        if (Vector3.Distance(centre.position, target.transform.position) < maxRadius)
        {
            speechBox.SetActive(true);
            answering = true;
        }
        else { speechBox.SetActive(false); answering = false; }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centre.position, maxRadius);
    }

    public void SpeakTo(int note)
    {

        print("answering");
        if (note == 3)
        {
            Pass();
        }
        else
        {
            Denied();
        }

        print(NoteManager.instance.currentNoteID);

    }

    void Pass()
    {
        answering = false;
        bubbles[0].SetActive(false);
        bubbles[1].SetActive(false);
        bubbles[2].SetActive(true);
        print("Pass");
    }

    void Denied()
    {
        answering = false;
        bubbles[0].SetActive(false);
        bubbles[1].SetActive(true);
        bubbles[2].SetActive(false);
        print("Fail");

    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(5);
        answering = false;

    }

}
