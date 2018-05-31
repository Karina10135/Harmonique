using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Burner : MonoBehaviour
{
    public GameObject noNote;
    public Text leafCountText;
    public int maxLeafCount;
    public ParticleSystem smoke;
    int currentCount;
    bool completed;

    private void Start()
    {
        currentCount = maxLeafCount;
        leafCountText.text = currentCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Leaf"))
        {
            currentCount--;
            smoke.Play();
            leafCountText.text = currentCount.ToString();
            other.GetComponent<Leaf>().BurnLeaf();
            if (currentCount == 0)
            {
                noNote.SetActive(true);
            }
        }
    }

    public void BurnTrigger()
    {
        smoke.Play();
    }

}
