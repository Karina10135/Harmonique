using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public bool dialog;
    public bool recording;
    public bool moleSequence;

    public static GameManager GM;



    private void Awake()
    {
        GM = this;
    }

    public void Start()
    {
    }

    public void SceneChange(string name)
    {

        SceneManager.LoadScene(name);
        
    }

    public void UpdatePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

}
