using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    void ButtonClick()
    {
        Fabric.EventManager.Instance.PostEvent("UI/Select", Camera.main.gameObject);
    }

    void HoverTrigger()
    {
        Fabric.EventManager.Instance.PostEvent("UI/Hover", Camera.main.gameObject);
    }
}
