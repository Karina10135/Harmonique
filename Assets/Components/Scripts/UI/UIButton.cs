using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject selectedNote;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        //Debug.Log("Cursor Entering " + name + " GameObject");

        Fabric.EventManager.Instance.PostEvent("UI/Hover", Camera.main.gameObject);

        if(selectedNote != null)
        {
            selectedNote.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        }

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(selectedNote != null)
        {
            selectedNote.GetComponent<Image>().sprite = NoteManager.instance.noteImages[NoteManager.instance.currentNoteID].sprite;
        }
    }



    public void ButtonClick()
    {
        Fabric.EventManager.Instance.PostEvent("UI/Select", Camera.main.gameObject);
    }

    public void HoverTrigger()
    {
        Fabric.EventManager.Instance.PostEvent("UI/Hover", Camera.main.gameObject);
    }
}
