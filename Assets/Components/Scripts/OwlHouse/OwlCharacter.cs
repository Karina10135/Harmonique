using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlCharacter : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject[] sentences;
    public int currentState;
    bool complete;

    private void Start()
    {
        complete = false;
    }

    private void Update()
    {
        OwlClick();
    }

    public void OwlTrigger()
    {
        if (!complete)
        {
            currentState = 0;
            foreach (GameObject bubble in sentences)
            {
                bubble.SetActive(false);
            }
            speechBubble.SetActive(true);
            sentences[currentState].SetActive(true);
        }
        else
        {
            Completed();
        }
    }

    public void NextSentence()
    {
        print("next");

        if (complete)
        {
            if(currentState < sentences.Length)
            {
                currentState++;
                sentences[currentState].SetActive(true);
            }
            else
            {
                speechBubble.SetActive(false);
            }

            print("Complete");
        }

        if (!complete)
        {
            if (currentState > 5)
            {
                speechBubble.SetActive(false);
                return;
            }

            foreach (GameObject bubble in sentences)
            {
                bubble.SetActive(false);
            }

            currentState++;
            sentences[currentState].SetActive(true);
            print("Incomplete");
        }

        print("talking");
        
    }

    public void OwlClick()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(mouseRay, out hit, 200))
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.CompareTag("Owl"))
                {
                    OwlTrigger();
                }
            }
            
        }
    }

    public void Completed()
    {
        complete = true;
        foreach (GameObject bubble in sentences)
        {
            bubble.SetActive(false);
        }
        speechBubble.SetActive(true);
        sentences[7].SetActive(true);
        currentState = 6;
    }
	
}
