using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public bool fadeIn;
    public bool fadeOut;
    public float speed;
    public Image fadeScreen;

	void Start ()
    {
        fadeScreen.canvasRenderer.SetAlpha(0.0f);

    }
	
	void Update ()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        if (fadeOut)
        {
            FadeOut();
        }
	}

    public void FadeIn()
    {
        fadeIn = true;
        fadeScreen.CrossFadeAlpha(1f, speed, false);
    }

    public void FadeOut()
    {
        fadeScreen.CrossFadeAlpha(0f, speed, false);
    }
}
