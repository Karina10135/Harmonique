using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, IPointerEnterHandler
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Fabric.EventManager.Instance.PostEvent("UI/Hover", Camera.main.gameObject);
        //Debug.Log("Cursor Entering " + name + " GameObject");
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
