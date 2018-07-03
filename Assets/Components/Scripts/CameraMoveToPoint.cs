using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMoveToPoint : MonoBehaviour
{
    public GameObject Player;
    public float stepSpeed = 10;
    public float fadeSpeed;
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
    public GameObject fadeScreen;

    [HideInInspector]
    public bool moveAble;
    public bool fading;
    float fadeValue;


    void Start ()
    {
        moveAble = true;
        Fabric.EventManager.Instance.PostEvent("Background/Main", gameObject);
    }

    void Update()
    {

        //input and set where to move to if not paused
        if (Input.GetKeyDown(pauseButton) && !isPaused)
        {
            //Player.GetComponent<KAM3RA.Actor>().Reset();

            if (moveAble)
            {
                originalPosition = transform;
                pausePanel.SetActive(true);
                currentPoint = 0;
                movePlace = movePoints[currentPoint].transform;
                
            }

            isPaused = true;
            pausePanel.SetActive(true);
            playerUI.SetActive(false);

            //gameObject.GetComponent<KAM3RA.User>().enabled = false;
            ActivePlayer(false);
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
                    //gameObject.GetComponent<KAM3RA.User>().enabled = true;
                    ActivePlayer(true);
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
        //gameObject.GetComponent<KAM3RA.User>().enabled = true;
        ActivePlayer(true);


        pausePanel.SetActive(false);
        playerUI.SetActive(true);
        isPaused = false;

    }

    public void FadeTransition()
    {
        //gameObject.GetComponent<KAM3RA.User>().enabled = false;
        //Player.GetComponent<KAM3RA.Actor>().enabled = false;
        //Player.GetComponent<KAM3RA.Actor>().Reset();
        ActivePlayer(false);
        


        fading = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        var c = fadeScreen.GetComponent<CanvasGroup>();

        while (fadeValue < 1f)
        {
            fadeValue += Time.deltaTime * fadeSpeed;
            c.alpha = fadeValue;
            if(fadeValue >= 1f)
            {
                fadeValue = 1f;
                ActivePlayer(true);
                StartCoroutine(FadeOut());
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.5f);
        ActivePlayer(false);
        yield return new WaitForSeconds(.5f);
        var c = fadeScreen.GetComponent<CanvasGroup>();

        while (fadeValue > 0f)
        {
            fadeValue -= Time.deltaTime * fadeSpeed;
            c.alpha = fadeValue;

            //if(fadeValue == 0.8f)
            //{
            //    Player.GetComponent<KAM3RA.Actor>().enabled = true;
            //    gameObject.GetComponent<KAM3RA.User>().enabled = true;
            //}

            if (fadeValue <= 0f)
            {
                fadeValue = 0f;
                //Player.GetComponent<KAM3RA.Actor>().enabled = true;
                //gameObject.GetComponent<KAM3RA.User>().enabled = true;
                ActivePlayer(true);
                fading = false;
                yield break;
            }

            yield return null;
        }
    }

    public void ActivePlayer(bool state)
    {
        //Player.GetComponent<KAM3RA.Actor>().Reset();
        //Player.GetComponent<KAM3RA.Actor>().enabled = state;
        //gameObject.GetComponent<KAM3RA.User>().enabled = state;
        gameObject.GetComponent<CameraController>().enabled = state;
        gameObject.GetComponent<CameraOcclusionProtector>().enabled = state;
    }



    public void QuitGame()
    {
        Application.Quit();
    }

}

