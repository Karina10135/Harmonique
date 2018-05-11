using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public bool dialog;
    public bool recording;
    public bool moleSequence;

    public GameObject player;
    public GameObject otherCam;

    public static GameManager GM;



    private void Awake()
    {
        GM = this;
    }

    public void CameraChange(bool l)
    {

        if(l == true)
        {
            player.SetActive(false);
            otherCam.SetActive(true);
        }
        else
        {
            player.SetActive(true);
            otherCam.SetActive(false);
        }
        

        

    }

}
