﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject selectedNote;
    public int buttonID;
    public bool hover;

    private void Start()
    {
    }

    void Update()
    {
        if(selectedNote == null) { return; }
            if (hover)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    hover = false;
                    NoteManager.instance.SelectNote(buttonID);
                    selectedNote.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                }
            }

        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Fabric.EventManager.Instance.PostEvent("UI/Hover", Camera.main.gameObject);
        hover = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hover = false;
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
