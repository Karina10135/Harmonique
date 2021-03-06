﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject mainPlayer;
    public Transform[] swapPositions;
    public Transform[] mainTransform;
    public Transform setTransform;
    public MoleGuard guard;
    public MoleManager moles;
    public OwlHouse owlHouse;
    public GraveyardRiddle grave;
    public GatePuzzle gate;
    public NoteManager note;


    public static PuzzleManager instance;


    private void Start()
    {
        instance = this;
        SetNoteManager();
        if (instance != this) { Destroy(gameObject); }
    }

    public void SetNoteManager()
    {
        note.grave = grave;
        note.gate = gate;
        note.guard = guard;
        note.moles = moles;
        note.owlHouse = owlHouse;
    }

    public void ChangePlayer(int play)
    {
        if(play == 1)
        {
            setTransform = mainTransform[0];
        }
        if (play == 2)
        {
            setTransform = mainTransform[1];
        }


       

        if(play == 0) //Outside
        {
            mainPlayer.transform.position = setTransform.position;
            Camera.main.GetComponent<CameraMoveToPoint>().moveAble = true;
            mainPlayer.GetComponent<Character>().inDoors = false;
            Fabric.EventManager.Instance.PostEvent("Background/Main", Fabric.EventAction.PlaySound, Camera.main.gameObject);
            Fabric.EventManager.Instance.PostEvent("Background/Interior", Fabric.EventAction.StopSound, Camera.main.gameObject);
            Camera.main.GetComponent<CameraMoveToPoint>().FadeTransition();
            Camera.main.GetComponent<CameraOcclusionProtector>().distanceToTarget = 32;

        }
        else //In Interior space
        {
            mainPlayer.transform.position = swapPositions[play].position;
            Camera.main.GetComponent<CameraMoveToPoint>().moveAble = false;
            mainPlayer.GetComponent<Character>().inDoors = true;
            owlHouse.owl.GetComponent<OwlCharacter>().OwlTrigger();
            Fabric.EventManager.Instance.PostEvent("Background/Main", Fabric.EventAction.StopSound, Camera.main.gameObject);
            Fabric.EventManager.Instance.PostEvent("Background/Interior", Fabric.EventAction.PlaySound, Camera.main.gameObject);
            Camera.main.GetComponent<CameraMoveToPoint>().FadeTransition();
            Camera.main.GetComponent<CameraOcclusionProtector>().distanceToTarget = 15;

        }


    }


    

}
