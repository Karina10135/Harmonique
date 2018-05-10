using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] NoteSounds;

    public AudioSource source;

    public void PlayAudio(AudioClip clip, AudioSource otherSource)
    {
        otherSource.clip = clip;
        otherSource.Play();
    }

    public void PlayNoteAudio(int note)
    {
        source.clip = NoteSounds[note];
        source.Play();
    }
}
