using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject mainPlayer;
    public GameObject[] players;
    public Transform[] swapPositions;
    public Transform[] mainTransform;
    public Transform setTransform;
    public MoleGuard guard;
    public MoleManager moles;
    public OwlHouse owlHouse;
    public GraveyardRiddle grave;
    public GatePuzzle gate;
    public KAM3RA.User cameraUser;
    public NoteManager note;


    public static PuzzleManager instance;

    public void Awake()
    {
        mainPlayer = players[0];
    }

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
            Fabric.EventManager.Instance.PostEvent("Background/Main", Fabric.EventAction.PlaySound, Camera.main.gameObject);
            Fabric.EventManager.Instance.PostEvent("Background/Interior", Fabric.EventAction.StopSound, Camera.main.gameObject);
            Camera.main.GetComponent<CameraMoveToPoint>().FadeTransition();

        }
        else //In Interior space
        {
            mainPlayer.transform.position = swapPositions[play].position;
            Camera.main.GetComponent<CameraMoveToPoint>().moveAble = false;
            Fabric.EventManager.Instance.PostEvent("Background/Main", Fabric.EventAction.StopSound, Camera.main.gameObject);
            Fabric.EventManager.Instance.PostEvent("Background/Interior", Fabric.EventAction.PlaySound, Camera.main.gameObject);
            Camera.main.GetComponent<CameraMoveToPoint>().FadeTransition();

        }


    }


    

}
