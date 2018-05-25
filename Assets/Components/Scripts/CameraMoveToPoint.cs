using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveToPoint : MonoBehaviour {

    public float stepSpeed = 10;
    public GameObject[] movePoints;
    public KeyCode pauseButton;

    private Transform movePlace;
    private Transform originalPosition;
    private int currentPoint;
    public bool isPaused = false;

    public GameObject ControlsPanel;
    public bool pauseAble;

    void Start ()
    {

    }

    void Update()
    {
        //input and set where to move to if not paused
        if (Input.GetKeyDown(pauseButton) && !isPaused)
        {

            if (pauseAble)
            {
                originalPosition = transform;
                currentPoint = 0;
                movePlace = movePoints[currentPoint].transform;
            }
            else
            {
                ControlsPanel.SetActive(true);

            }

            isPaused = true;


        }

        //move back to original position when unpaused
        if (Input.GetKeyDown(pauseButton) && isPaused)
        {
            if (pauseAble)
            {
                movePlace = originalPosition;
                
            }
            else
            {
                ControlsPanel.SetActive(true);
            }

            isPaused = false;

        }

        if (isPaused)
        {
            //if has point to move to then move
            if (movePlace != null)
            {
                float step = stepSpeed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, movePlace.position, step);
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
        movePlace = movePoints[currentPoint++].transform;
    }

    public void StepDownPoints()
    {
        movePlace = movePoints[currentPoint--].transform;
    }

    public void Resume()
    {
        movePlace = originalPosition;
        isPaused = false;
    }

}

