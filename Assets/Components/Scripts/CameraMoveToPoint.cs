using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraMoveToPoint : MonoBehaviour {

    public float stepSpeed = 10;
    public GameObject[] movePoints;
    public KeyCode pauseButton;

    public Transform movePlace;
    public Transform originalPosition;
    private int currentPoint;
    public bool isPaused = false;

    public GameObject pausePanel;
    public GameObject playerUI;
    //[HideInInspector]
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
                //originalPosition = transform;
                pausePanel.SetActive(true);
                currentPoint = 0;
                movePlace = movePoints[currentPoint].transform;
                
            }

            isPaused = true;
            pausePanel.SetActive(true);
            playerUI.SetActive(false);

            gameObject.GetComponent<KAM3RA.User>().enabled = false;
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
                pausePanel.SetActive(false);
                playerUI.SetActive(true);
                isPaused = false;

            }

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
            else if(movePlace == null)
            {
                return;
            }

            //if has reached point stop move
            if (transform.position == movePlace.position)
            {
                if(movePlace.position == originalPosition.position)
                {
                    gameObject.GetComponent<KAM3RA.User>().enabled = true;
                    isPaused = false;
                }
                movePlace = null;
            }

        }

        


    }

    public void StepUpPoints()
    {
        if (!moveAble) { return; }
        currentPoint++;
        movePlace = movePoints[currentPoint].transform;
    }

    public void StepDownPoints()
    {
        if (!moveAble) { return; }
        currentPoint--;
        movePlace = movePoints[currentPoint].transform;
    }

    public void Resume()
    {
        movePlace = originalPosition;
        //StartCoroutine(ReturnTimer());
        pausePanel.SetActive(false);
        playerUI.SetActive(true);

    }

    public IEnumerator ReturnTimer()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<KAM3RA.User>().enabled = true;
        isPaused = false;


    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main");
    }

}

