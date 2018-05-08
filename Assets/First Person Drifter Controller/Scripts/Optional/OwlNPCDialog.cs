using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlNPCDialog : MonoBehaviour
{
    public GameObject[] dialogBox;
    int state;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PopUpDialog();
        }
    }

    void PopUpDialog()
    {
        state++;

        foreach (GameObject box in dialogBox)
        {
            box.SetActive(false);
        }

        if(state == dialogBox.Length)
        {
            ExitDialog();
            return;
        }

        dialogBox[state].SetActive(true);
    }

    void ExitDialog()
    {
        foreach(GameObject box in dialogBox)
        {
            box.SetActive(false);
        }
    }

}
