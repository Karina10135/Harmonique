using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public MoleGuard guard;
    public YesNote grave;

    public static PuzzleManager instance;

    private void Start()
    {
        instance = this;
    }


}
