using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGuard : MonoBehaviour
{

    public GameObject noSpeechBubble;
    public GameObject yesSpeechBubble;
    bool answering;

    public void DialogTrigger()
    {
        answering = true;
        print("Talking");
    }

    public void SpeakTo(int note)
    {
        if (answering)
        {
            print("answering");
            if (note == 4)
            {
                Pass();
            }
            else
            {
                Denied();
            }
        }
        else
        {
            DialogTrigger();
        }
        
    }

    void Pass()
    {
        yesSpeechBubble.SetActive(true);
        FadeTime();
    }

    void Denied()
    {
        noSpeechBubble.SetActive(true);
        FadeTime();

    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(5);
        noSpeechBubble.SetActive(false);
        yesSpeechBubble.SetActive(false);
        answering = false;

    }

}
