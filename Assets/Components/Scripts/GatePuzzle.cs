using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatePuzzle : MonoBehaviour
{
    public ParticleSystem confeteVFX;
    public GameObject[] trumpets;
    public ParticleSystem[] gateNoteVFX;
    public int[] noteSequence;

    public GameObject[] creditsPanel;
    public float fadeCreditsIntro; //How long til credits start after completion
    public float fadeSpeed; //The speed of the fade in/out
    public float creditsTimer; //How long it pauses on a panel
    public float creditsOutTimer; //The pause outside of credits.
    int currentPanel;
    float val;
    bool fading;

    public int currentNote;
    public int noteNum;
    public bool complete;
    public Animator anim;
    public NoteManager noteManager;

    private void Start()
    {
        noteManager = NoteManager.instance;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    CompletedPuzzle();
        //}
    }

    public void CheckNote(int note)
    {
        if (complete) { return; }
        if (note != currentNote) { ResetSequence(); return; }


        PlayTrumpet(noteNum);

        if (noteNum == 4)
        {
            CompletedPuzzle();
            return;
        }

        noteNum++;
        AssignNote();

    }

    public void ResetSequence()
    {
        noteNum = 0;
        AssignNote();
    }

    public void AssignNote()
    {

        currentNote = noteSequence[noteNum];
    }

    public void PlayTrumpet(int i)
    {
        ParticleSystem trumpet = Instantiate(confeteVFX, trumpets[i].transform);
        trumpet.GetComponentInChildren<ParticleSystem>().Play();
        gateNoteVFX[i].Play();
    }


    public void CompletedPuzzle()
    {
        complete = true;
        GameManager.GM.gameOver = true;
        Camera.main.GetComponent<CameraMoveToPoint>().EndGame();
        Fabric.EventManager.Instance.PostEvent("Misc/Melodygatesuccess", gameObject);
        anim.SetBool("Complete", true);
        StartCoroutine(StartCredits());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            noteManager.gateRelay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            noteManager.gateRelay = false;
        }
    }

    IEnumerator StartCredits()
    {
        yield return new WaitForSeconds(fadeCreditsIntro);
        Fabric.EventManager.Instance.PostEvent("Background/Main", Fabric.EventAction.StopSound, Camera.main.gameObject);
        Fabric.EventManager.Instance.PostEvent("Background/Credits", gameObject);
        StartCoroutine(PanelTimer(true, 0));
    }

    IEnumerator PanelTimer(bool fade, int panel)
    {

        var c = creditsPanel[panel].GetComponent<CanvasGroup>();

        if (fade)
        {
            while (val < 1)
            {

                val += Time.deltaTime * fadeSpeed;
                c.alpha = val;
                if(val >= 1f)
                {
                    val = 1f;
                    StartCoroutine(PauseTimer());
                    yield break;
                }
                yield return null;
            }
        }
        else
        {
            while (val > 0)
            {
                val -= Time.deltaTime * fadeSpeed;
                c.alpha = val;
                if (val <= 0f)
                {
                    val = 0f;
                    yield return new WaitForSeconds(creditsOutTimer);
                    NextPanel();
                    yield break;
                }
                yield return null;
            }
        }
        
    }

    IEnumerator PauseTimer()
    {
        var i = creditsTimer;
        if(currentPanel == 0)
        {
            i = creditsTimer / 2;
        }
        yield return new WaitForSeconds(i);
        StartCoroutine(PanelTimer(false, currentPanel));

    }

    public void NextPanel()
    {
        if(currentPanel == creditsPanel.Length - 1)
        {
            //GameManager.GM.SceneChange("Start");
            GameManager.GM.FadingOutOfScene("Start");
            return;
        }
        currentPanel++;
        StartCoroutine(PanelTimer(true, currentPanel));


    }

}
