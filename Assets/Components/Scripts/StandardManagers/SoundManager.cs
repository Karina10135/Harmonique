using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] NoteSounds;

    public AudioSource source;
    public static SoundManager instance;


    public void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioSource otherSource, AudioClip clip)
    {
        otherSource.clip = clip;
        otherSource.Play();
    }

    public void PlayNoteAudio(int note)
    {
        source.clip = NoteSounds[note];
        source.Play();
    }

    public void StopAudio()
    {
        source.Stop();
    }
}
