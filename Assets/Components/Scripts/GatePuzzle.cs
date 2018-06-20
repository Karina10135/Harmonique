using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatePuzzle : MonoBehaviour
{
    public GameObject confete;
    public GameObject[] trumpets;
    public int[] noteSequence;

    public float fadeTimer;
    public GameObject[] creditsPanel;
    public float fadeSpeed;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            val = 1f;
            StartCoroutine(PanelTimer(false));
        }
    }

    public void StartSequence()
    {
        
    }

    public void CheckNote(int note)
    {
        if (complete) { return; }
        if (note != currentNote) { ResetSequence(); return; }


        PlayTrumpet(trumpets[currentNote].transform);

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
        print(currentNote);
    }

    public void PlayTrumpet(Transform position)
    {
        GameObject trumpet = Instantiate(confete, position);
        trumpet.GetComponentInChildren<ParticleSystem>().Play();
    }


    public void CompletedPuzzle()
    {
        Fabric.EventManager.Instance.PostEvent("Misc/Melodygatesuccess", gameObject);
        anim.SetBool("Complete", true);
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



    IEnumerator PanelTimer(bool fade)
    {
        yield return new WaitForSeconds(1);
        print("fade timer");
        var c = creditsPanel[0].GetComponent<CanvasGroup>();

        if (fade)
        {
            while (val < 1)
            {
                val += Time.deltaTime * fadeSpeed;
                c.alpha = val;
                if(val >= 1f)
                {
                    val = 1f;
                    NextPanel();

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
                print(val);
                if (val <= 0f)
                {
                    val = 0f;
                    NextPanel();

                    yield break;
                }
                yield return null;
            }
        }
        


        //fade out of current panel
        //yield return new WaitForSeconds(fadeTimer);
        //full visible panel
        //pause to read timer
        //fade to next panel
    }

    public void NextPanel()
    {
        if(currentPanel == creditsPanel.Length)
        {

        }
    }

}
