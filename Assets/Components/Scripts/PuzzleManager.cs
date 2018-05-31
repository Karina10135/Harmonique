using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject mainPlayer;
    public GameObject[] players;
    public MoleGuard guard;
    public MoleManager moles;
    public OwlHouse owlHouse;
    public GraveyardRiddle grave;
    public GatePuzzle gate;
    public KAM3RA.User cameraUser;
    public static PuzzleManager instance;

    public void Awake()
    {
        mainPlayer = players[0];
    }

    private void Start()
    {
        instance = this;
        if(instance != this) { Destroy(gameObject); }
    }

    public void ChangePlayer(int play)
    {
        mainPlayer = players[play];
        cameraUser.player = mainPlayer.GetComponent<KAM3RA.Actor>();

    }


}
