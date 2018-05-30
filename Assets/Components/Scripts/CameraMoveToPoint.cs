using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraMoveToPoint : MonoBehaviour {

    public float stepSpeed = 10;
    public GameObject[] movePoints;
    public KeyCode pauseButton;

    private Transform movePlace;
    private Transform originalPosition;
    private int currentPoint;
    public bool isPaused = false;

    public GameObject pausePanel;
    [HideInInspector]
    public bool moveAble;

    void Start ()
    {
        moveAble = true;
    }

    void Update()
    {
        //input and set where to move to if not paused
        if (Input.GetKeyDown(pauseButton) && !isPaused)
        {

            if (moveAble)
            {
                pausePanel.SetActive(true);
                originalPosition = transform;
                currentPoint = 0;
                movePlace = movePoints[currentPoint].transform;
                
            }
            else
            {
                pausePanel.SetActive(true);

            }

            isPaused = true;
            gameObject.GetComponent<KAM3RA.User>().enabled = false;
            print("paused");
            return;

        }

        //move back to original position when unpaused
        if (Input.GetKeyDown(pauseButton) && isPaused)
        {
            if (moveAble)
            {
                movePlace = originalPosition;
                Resume();


            }
            else
            {
                pausePanel.SetActive(true);
            }

            isPaused = false;
            print("unpaused");
        }

        if (isPaused && moveAble)
        {
            //if has point to move to then move

            if (movePlace != null)
            {
                float step = stepSpeed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, movePlace.position, step);
                transform.rotation = Quaternion.Slerp(transform.rotation, movePlace.rotation, Time.deltaTime * step);


            }

            //if has reached point stop move
            if (transform.position == movePlace.position)
            {
                movePlace = null;
            }

        }

        


    }

    public void StepUpPoints()
    {
        if (!moveAble) { return; }
        movePlace = movePoints[currentPoint++].transform;
    }

    public void StepDownPoints()
    {
        if (!moveAble) { return; }
        movePlace = movePoints[currentPoint--].transform;
    }

    public void Resume()
    {
        gameObject.GetComponent<KAM3RA.User>().enabled=true;
        movePlace = originalPosition;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main");
    }

}

