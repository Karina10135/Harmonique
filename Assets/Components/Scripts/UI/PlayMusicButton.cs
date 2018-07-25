using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayMusicButton : MonoBehaviour, IPointerClickHandler
{
    public CameraMoveToPoint cam;
    public NoteManager note;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (cam.isPaused) { return; }

        note.PlayNote();
        print("I HAVE BEEN CLICKED");
    }



}
