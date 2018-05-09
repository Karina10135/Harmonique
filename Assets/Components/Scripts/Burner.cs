using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{
    public int maxLeafCount;
    public ParticleSystem smoke;

    int currentCount;
    bool completed;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		if(currentCount == maxLeafCount)
        {
            NoteManager.instance.NoteAvailable(3);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Leaf"))
        {
            currentCount++;
            other.GetComponent<Leaf>().BurnLeaf();
        }
    }

    public void BurnTrigger()
    {
        smoke.Play();
    }

}
