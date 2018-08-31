using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
    public GameObject[] NoteVFX;
    public GameObject[] TrumpetVFX;


    public void PlayNoteSound(int NoteId)
    {
        NoteVFX[NoteId - 1].GetComponent<ParticleSystem>().Play();
        TrumpetVFX[NoteId - 1].GetComponent<ParticleSystem>().Play();
        string note = "UINote/" + NoteId.ToString();
        Fabric.EventManager.Instance.PostEvent(note, Camera.main.gameObject);

    }


}
