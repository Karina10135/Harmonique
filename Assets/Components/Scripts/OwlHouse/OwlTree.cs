using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlTree : MonoBehaviour
{
    public GameObject target;
    public GameObject dialogBox;
    public GameObject treeLight;
    public float minRadius;
    public float maxRadius;
    public Transform centre;
    int state;

    private void Start()
    {
        
    }

    private void Update()
    {
        CheckCollision();
    }
    
    public void CheckCollision()
    {
        if(Vector3.Distance(centre.position, target.transform.position) < minRadius || Vector3.Distance(centre.position, target.transform.position) > maxRadius)
        {
            dialogBox.SetActive(false);
        }
        if(Vector3.Distance(centre.position, target.transform.position) < maxRadius && Vector3.Distance(centre.position, target.transform.position) > minRadius)
        {
            dialogBox.SetActive(true);

        }
    }

    
    public void SetTreeLight(bool state)
    {

        if(treeLight == null) { return; }
        treeLight.SetActive(state);
    }

    #region SpeechBubblePopUp
    void ExitDialog()
    {
        dialogBox.SetActive(false);
        state = 0;
    }

    void OwlDialog()
    {
        dialogBox.SetActive(true);
        StartCoroutine(Timer());

    }

    void PopUp()
    {
        state = 1;
        StartCoroutine(Timer());
    }

#endregion

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
