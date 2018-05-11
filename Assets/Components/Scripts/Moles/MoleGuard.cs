using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGuard : MonoBehaviour
{

    public string[] answers;
    string sent;
    public bool answering;

    private void Start()
    {
        sent = GetComponent<DialogueTrigger>().dialogue.sentences[1];

    }

    public void DialogTrigger()
    {
        answering = true;
        print("Talking");
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

        print(NoteManager.instance.currentNoteID);


        //if (answering)
        //{
        //    print("answering");
        //    if (note == 4)
        //    {
        //        Pass();
        //    }
        //    else
        //    {
        //        Denied();
        //    }
        //}
        //else
        //{
        //    DialogTrigger();
        //}

    }

    void Pass()
    {
        gameObject.GetComponent<DialogueTrigger>().dialogue.sentences[1] = answers[0];
        answering = false;
        print("Pass");
    }

    void Denied()
    {
        gameObject.GetComponent<DialogueTrigger>().dialogue.sentences[1] = answers[1];
        answering = false;
        print("Fail");

    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(5);
        answering = false;

    }

}
