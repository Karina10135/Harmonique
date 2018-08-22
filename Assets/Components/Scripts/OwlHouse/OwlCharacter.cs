using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlCharacter : MonoBehaviour
{
    public float interactDistance;

    public GameObject speechBubble;
    public GameObject[] sentences;
    public int currentState;
    bool complete;
    GameObject player;

    private void Start()
    {
        complete = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //OwlClick();
        ReplyClick();
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

    public void ReplyClick()
    {
        float dst = Vector3.Distance(player.transform.position, transform.position);
        if(dst < interactDistance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                NextSentence();
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

    public void NextSentence()
    {

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
        }

        
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
