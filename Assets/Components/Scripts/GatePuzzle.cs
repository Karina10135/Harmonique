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

    public float fadeTimer;
    public GameObject[] creditsPanel;
    public float fadeSpeed;
    public float creditsTimer;
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
        Fabric.EventManager.Instance.PostEvent("Misc/Melodygatesuccess", gameObject);
        anim.SetBool("Complete", true);
        StartCoroutine(PanelTimer(true, 0));
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



    IEnumerator PanelTimer(bool fade, int panel)
    {
        if(currentPanel == 0)
        {
            yield return new WaitForSeconds(5);
        }

        yield return new WaitForSeconds(creditsTimer);
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
                    NextPanel();
                    yield break;
                }
                yield return null;
            }
        }
        
    }

    IEnumerator PauseTimer()
    {
        yield return new WaitForSeconds(creditsTimer);
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
