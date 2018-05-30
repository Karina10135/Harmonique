using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{
    public GameObject noNote;
    public int maxLeafCount;
    public ParticleSystem smoke;
    int currentCount;
    bool completed;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Leaf"))
        {
            currentCount++;
            other.GetComponent<Leaf>().BurnLeaf();
            if (currentCount == maxLeafCount)
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
