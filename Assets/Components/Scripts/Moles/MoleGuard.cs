using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGuard : MonoBehaviour
{
    public GameObject speechBox;
    public GameObject[] bubbles;

    public GameObject caveDoor;
    public float maxRadius;
    public Transform centre;
    public GameObject target;
    public bool answering;

    NoteManager note;

    private void Start()
    {

        note = NoteManager.instance;

    }
    private void Update()
    {
        CheckCollision();
    }

    public void CheckCollision()
    {
        if (Vector3.Distance(centre.position, target.transform.position) < maxRadius)
        {
            speechBox.SetActive(true);
            answering = true;
            note.answeringMoleGuard = true;
        }
        else
        {
            ResetSpeech();
            speechBox.SetActive(false);
            answering = false;
            note.answeringMoleGuard = false;
        }
        
    }

    public void ResetSpeech()
    {
        bubbles[0].SetActive(true);
        bubbles[1].SetActive(false);
        bubbles[2].SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centre.position, maxRadius);
    }

    public void SpeakTo(int note)
    {

        print("answering");
        if (note == 3)
        {
            Pass();
        }
        else
        {
            Denied();
        }

    }

    void Pass()
    {
        answering = false;
        bubbles[0].SetActive(false);
        bubbles[1].SetActive(false);
        bubbles[2].SetActive(true);
        caveDoor.GetComponent<BoxCollider>().enabled = true;
        print("Completed");
    }

    void Denied()
    {
        answering = false;
        bubbles[0].SetActive(false);
        bubbles[1].SetActive(true);
        bubbles[2].SetActive(false);
        print("Fail");

    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(5);
        answering = false;

    }

}
