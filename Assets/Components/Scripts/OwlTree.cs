using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlTree : MonoBehaviour
{
    
    public GameObject dialogBox;
    Text log;
    bool dialog;
    int state;

    private void Start()
    {
        log = dialogBox.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (!dialog) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            if(state != 1)
            {
                PopUp();

            }
            else
            {
                ExitDialog();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            dialog = true;
            OwlDialog();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ExitDialog();
        }
    }

    void ExitDialog()
    {
        dialogBox.SetActive(false);
        GameManager.GM.dialog = false;
        state = 0;
    }

    void OwlDialog()
    {
        GameManager.GM.dialog = true;
        dialogBox.SetActive(true);
        log.text = "Hoot whose out there!!";

    }

    void PopUp()
    {
        log.text = "Hoot help!!";
        state = 1;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

    }


}
