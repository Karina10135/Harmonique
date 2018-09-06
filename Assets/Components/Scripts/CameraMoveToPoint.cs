using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMoveToPoint : MonoBehaviour
{
    public GameObject Player;
    public GameObject cameraRig;
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
    public GameObject tutorialPanel;
    public GameObject fadeScreen;

    [HideInInspector]
    public bool moveAble;
    public bool fading;
    float fadeValue;
    int escPress;

    void Start ()
    {
        moveAble = true;
        LockCursor(false);
        Cursor.lockState = CursorLockMode.Locked;
        Fabric.EventManager.Instance.PostEvent("Background/Main", gameObject);
        GameManager.GM.gameOver = false;
        GameManager.GM.FadingInToScene();
        escPress = 0;
    }

    void Update()
    {

        

        if (!isPaused)
        {

            if (Input.GetMouseButtonDown(1))
            {
                LockCursor(true);
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetMouseButtonUp(1))
            {
                LockCursor(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }


        if (GameManager.GM.gameOver)
        {
            return;
        }


        //input and set where to move to if not paused
        if (Input.GetKeyDown(pauseButton) && !isPaused)
        {
            escPress++;
            LockCursor(true);

            if (moveAble)
            {
                pausePanel.SetActive(true);
                currentPoint = 0;
                movePlace = movePoints[currentPoint].transform;
                
            }

            isPaused = true;
            pausePanel.SetActive(true);
            tutorialPanel.GetComponent<Tutorial>().BoxState(false);
            playerUI.GetComponent<CanvasGroup>().alpha = 0;

            ActivePlayer(false);
            return;

        }


        //move back to original position when unpaused
        if (Input.GetKeyDown(pauseButton) && isPaused)
        {

            Resume();


            if (moveAble)
            {
                movePlace = originalPosition;
                return;

            }
            else
            {

                ActivePlayer(true);

                return;

            }

        }

        if (moveAble)
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
        //gameObject.GetComponent<CameraController>().target = movePlace;
    }

    public void StepDownPoints()
    {
        if (!moveAble) { return; }
        currentPoint--;
        movePlace = movePoints[currentPoint].transform;
        //gameObject.GetComponent<CameraController>().target = movePlace;
    }

    public void Resume()
    {
        escPress = 0;
        if (moveAble)
        {
            movePlace = originalPosition;
        }

        ActivePlayer(true);
        mainPausePanel.SetActive(true);
        controlPanel.SetActive(false);
        pausePanel.SetActive(false);

        tutorialPanel.GetComponent<Tutorial>().BoxState(true);

        playerUI.GetComponent<CanvasGroup>().alpha = 1;
        LockCursor(false);
        isPaused = false;
    }

    public void LockCursor(bool lockCursor)
    {
        //Screen.lockCursor = lockCursor;
        
        Cursor.visible = lockCursor;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void FadeTransition()
    {
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
        Player.GetComponent<Character>().ProcessAnimation(false);

        while (fadeValue > 0f)
        {
            fadeValue -= Time.deltaTime * fadeSpeed;
            c.alpha = fadeValue;


            if (fadeValue <= 0f)
            {
                fadeValue = 0f;
                
                ActivePlayer(true);
                Player.GetComponent<Character>().ProcessAnimation(false);
                fading = false;
                yield break;
            }

            yield return null;
        }
    }

    public void ActivePlayer(bool state)
    {
        Player.GetComponent<Character>().RoomChange();
        gameObject.GetComponent<CameraController>().enabled = state;
        gameObject.GetComponent<CameraOcclusionProtector>().enabled = state;
        if (state)
        {
            gameObject.GetComponent<CameraController>().target = Player.transform;
            gameObject.GetComponent<CameraController>().Activate();

        }
        else
        {
            gameObject.GetComponent<CameraController>().target = movePoints[0].transform;

        }

    }

    public void EndGame()
    {
        ActivePlayer(true);
        playerUI.GetComponent<CanvasGroup>().alpha = 1;
        LockCursor(false);
        mainPausePanel.SetActive(true);
        controlPanel.SetActive(false);
        pausePanel.SetActive(false);
        isPaused = false;

    }


    public void QuitGame()
    {
        Application.Quit();
    }

}

