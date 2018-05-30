using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public MoleGuard guard;
    public MoleManager moles;
    public OwlHouse owlHouse;
    public GraveyardRiddle grave;
    public GatePuzzle gate;

    public static PuzzleManager instance;

    public void Awake()
    {
        
    }

    private void Start()
    {
        instance = this;
        if(instance != this) { Destroy(gameObject); }
    }


}
