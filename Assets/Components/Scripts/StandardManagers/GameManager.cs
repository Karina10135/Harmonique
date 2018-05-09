using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public bool dialog;

    public static GameManager GM;



    private void Awake()
    {
        GM = this;
    }

}
