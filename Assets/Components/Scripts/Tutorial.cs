using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject textBox;
    public Text panelText;


    [TextArea(4,11)]
    public string[] instructions;

    public float waitTime;
    public float boxTimer;
    public int currentStep;
    public bool tutorialCompleted;
    Animator anim;
    bool timing;

	void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        StartTutorial();
	}

    void StartTutorial()
    {
        StartCoroutine(BoxTimer(instructions[currentStep]));

    }

    public void NextStep()
    {
        if (tutorialCompleted) { timing = false; return; }

        
        StopAllCoroutines();

        if(currentStep == instructions.Length - 1) { anim.SetBool("Opened", false); tutorialCompleted = true; gameObject.SetActive(false); }
        else
        {
            anim.SetBool("Opened", false);
            currentStep++;

            StartCoroutine(BoxTimer(instructions[currentStep]));
        }


    }
	
	void Update ()
    {

        if (timing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                NextStep();
            }
        }
        

        
	}

    IEnumerator BoxTimer(string text)
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Opened", true);
        yield return new WaitForSeconds(boxTimer);
        StartCoroutine(TypeSentence(text));
    }

    IEnumerator ClickTimer()
    {
        timing = false;
        yield return new WaitForSeconds(waitTime);
        timing = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        StartCoroutine(ClickTimer());
        panelText.text = "";
        print("Typing");
        foreach(char letter in sentence.ToCharArray())
        {
            panelText.text += letter;
            yield return null;
        }
    }
}
