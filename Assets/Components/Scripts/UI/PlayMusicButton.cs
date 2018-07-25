using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayMusicButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CameraMoveToPoint cam;
    public NoteManager note;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (cam.isPaused) { return; }
        if (!Input.GetMouseButtonDown(0)) { return; }


        note.PlayTrigger();
    }


    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        note.StopNote();
    }


}
