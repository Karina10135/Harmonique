﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject textBox;
    public Text panelText;

    [TextArea(4,11)]
    public string[] instructions;

    public float boxTimer;
    public int currentStep;
    public bool tutorialCompleted;
    Animator anim;

	void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        StartTutorial();
	}

    void StartTutorial()
    {
        StartCoroutine(BoxTimer(instructions[currentStep]));

    }

    public void NextStep(/*int step*/)
    {
        if (tutorialCompleted) { return; }

        //step = currentStep;
        StopAllCoroutines();
        if (currentStep < instructions.Length)
        {
            anim.SetBool("Opened", false);
            currentStep++;

            StartCoroutine(BoxTimer(instructions[currentStep]));
            //StartCoroutine(TypeSentence(instructions[currentStep]));

        }
        else { anim.SetBool("Opened", false); tutorialCompleted = true; }

    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
	}

    IEnumerator BoxTimer(string text)
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Opened", true);
        yield return new WaitForSeconds(boxTimer);
        StartCoroutine(TypeSentence(text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        panelText.text = "";
        print("Typing");
        foreach(char letter in sentence.ToCharArray())
        {
            panelText.text += letter;
            yield return null;
        }
    }
}
