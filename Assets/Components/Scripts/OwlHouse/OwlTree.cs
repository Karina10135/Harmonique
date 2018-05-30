using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlTree : MonoBehaviour
{
    public float minRadius;
    public float maxRadius;
    public GameObject dialogBox;
    public Transform centre;
    int state;
    public GameObject target;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centre.position, minRadius);
        Gizmos.DrawWireSphere(centre.position, maxRadius);
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
