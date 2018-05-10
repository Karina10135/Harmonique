using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlTree : MonoBehaviour
{
    
    public GameObject dialogBox;
    Text log;
    int state;

    private void Start()
    {
        log = dialogBox.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
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
        state = 0;
    }

    void OwlDialog()
    {
        dialogBox.SetActive(true);
        log.text = "Hoot whose out there!!";
        StartCoroutine(Timer());

    }

    void PopUp()
    {
        log.text = "Hoot help!!";
        state = 1;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
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
