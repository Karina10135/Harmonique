using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlTree : MonoBehaviour
{
    public GameObject target;
    public GameObject dialogBox;
    public GameObject[] dialogText;
    public GameObject treeLight;
    public float owlBubbleTimer;
    public float minRadius;
    public float maxRadius;
    public Transform centre;
    int state;
    bool player;
    float timer;

    private void Start()
    {
        timer = owlBubbleTimer;
    }

    private void Update()
    {
        CheckCollision();

        dialogBox.SetActive(player);
        if (player)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                OwlDialog();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(centre.position, minRadius);
        Gizmos.DrawWireSphere(centre.position, maxRadius);

    }

    public void CheckCollision()
    {
        if(Vector3.Distance(centre.position, target.transform.position) < minRadius || Vector3.Distance(centre.position, target.transform.position) > maxRadius)
        {
            player = false;
        }
        if(Vector3.Distance(centre.position, target.transform.position) < maxRadius && Vector3.Distance(centre.position, target.transform.position) > minRadius)
        {
            player = true;

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
        //dialogBox.SetActive(false);

        state = 0;
    }

    void OwlDialog()
    {
        //dialogBox.SetActive(true);
        foreach(GameObject text in dialogText)
        {
            text.SetActive(false);
        }
        dialogText[state].SetActive(true);

        if (state == 0)
        {
            state++;
            timer = owlBubbleTimer;
            return;
        }

        if (state == 1)
        {
            state--;
            timer = owlBubbleTimer;

            return;
        }
        //StartCoroutine(Timer());

    }

    void PopUp()
    {
        
        StartCoroutine(Timer());
    }

#endregion

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        foreach(GameObject text in dialogText)
        {
            text.SetActive(false);
        }

        dialogText[state].SetActive(true);

        if(state == 0)
        {
            state++;
            StartCoroutine(Timer());
            yield break;
        }

        if(state == 1)
        {
            state--;
            StartCoroutine(Timer());
            yield break;
        }
        

    }


}
