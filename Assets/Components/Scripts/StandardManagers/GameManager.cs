﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public bool dialog;
    public bool recording;
    public bool moleSequence;

    public GameObject fadeCanvas;
    public static GameManager GM;

    public AudioMixer musicMixer;
    public float minAudioValue;
    public float maxAudioValue;
    public float mixerSpeed;
    float targetValue;
    bool maxSound;
    //float soundValue;

    public float fadeSpeed;
    float val;

    private void Awake()
    {
        GM = this;
        if(GM != this) { Destroy(this.gameObject); }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        maxSound = true;
        targetValue = maxAudioValue;
    }

    private void Update()
    {
        SoundControl();

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TurnMixerDown(true);
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    TurnMixerDown(false);

        //}
    }
    void SoundControl()
    {
        if (maxSound)
        {
            if (targetValue > maxAudioValue)
            {
                targetValue = maxAudioValue;
            }
            else
            {
                targetValue += Time.deltaTime * mixerSpeed;
                
            }
        }
        else
        {
            if (targetValue < minAudioValue)
            {
                targetValue = minAudioValue;
            }
            else
            {
                targetValue -= Time.deltaTime * mixerSpeed;

            }
        }
            
        musicMixer.SetFloat("MusicDuck", targetValue);
    }

    public void SceneChange(string name)
    {

        SceneManager.LoadScene(name);
        
    }

    public void TurnMixerDown(bool down)
    {
        maxSound = !down;
    }

    public void UpdatePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FadingOutOfScene(string scene)
    {
        StartCoroutine(FadeTimer(true, scene));

    }

    public void FadingInToScene()
    {
        StartCoroutine(FadeTimer(false, "Main"));

    }

    IEnumerator FadeTimer(bool fade, string name)
    {
        var fadeCan = fadeCanvas.GetComponent<CanvasGroup>();
        yield return new WaitForSeconds(1f);
        val = fadeCan.alpha;

        if (fade)
        {
            while (val < 1f)
            {
                val += Time.deltaTime * fadeSpeed;
                fadeCan.alpha = val;

                if(val >= 1f)
                {
                    val = 1f;

                    if(name != null)
                    {
                        SceneManager.LoadScene(name);
                    }

                    yield break;
                }
                yield return null;
            }
        }
        else 
        {


            while (val > 0f)
            {

                val -= Time.deltaTime * fadeSpeed;
                fadeCan.alpha = val;

                if (val <= 0f)
                {
                    val = 0f;
                    fadeCan.alpha = val;
                    yield break;
                }
                yield return null;

            }
        }
        
    }

}
