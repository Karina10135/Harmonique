using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayMusicButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CameraMoveToPoint cam;
    public NoteManager note;

    private void Start()
    {
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (cam.isPaused) { return; }
        if (!Input.GetMouseButtonDown(0)) { return; }
        if (Input.GetMouseButtonDown(1)) { return; }

        note.PlayTrigger();
    }


    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        note.StopNote();
    }


}
