using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMoveToPoint : MonoBehaviour
{

    public float stepSpeed = 10;
    public float fadeTimer;
    public GameObject[] movePoints;
    public KeyCode pauseButton;

    public Transform movePlace;
    public Transform originalPosition;
    private int currentPoint;
    public bool isPaused = false;

    public GameObject pausePanel;
    public GameObject playerUI;
    public GameObject mainPausePanel;
    public GameObject controlPanel;
    public Image fadeScreen;

    //[HideInInspector]
    public bool moveAble;
    bool fading;
    public float fadeState;


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
                Resume();


            }
            else
            {
                Resume();
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

    public void FadeTimer(bool moveAble)
    {
        isPaused = true;
        StartCoroutine(FadeTimer(fadeTimer, moveAble));
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
        gameObject.GetComponent<KAM3RA.User>().enabled = true;

        //StartCoroutine(ReturnTimer());
        pausePanel.SetActive(false);
        playerUI.SetActive(true);
        isPaused = false;

    }

    public IEnumerator ReturnTimer()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<KAM3RA.User>().enabled = true;
        isPaused = false;


    }

    IEnumerator FadeTimer(float time, bool move)
    {
        gameObject.GetComponent<KAM3RA.User>().enabled = false;
        fadeScreen.CrossFadeAlpha(255f, fadeTimer, true);
        yield return new WaitForSeconds(2);
        //fadeScreen.CrossFadeAlpha(0f, fadeTimer, true);
        gameObject.GetComponent<KAM3RA.User>().enabled = true;
        moveAble = move;
        isPaused = false;
    }


    public void QuitGame()
    {
        SceneManager.LoadScene("Main");
    }

}

