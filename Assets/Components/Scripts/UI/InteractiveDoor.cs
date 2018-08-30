using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{

	void PlayNoteSound(int NoteId)
    {
        string note = "Note/" + NoteId.ToString();
        Fabric.EventManager.Instance.PostEvent(note, Camera.main.gameObject);
    }


}
