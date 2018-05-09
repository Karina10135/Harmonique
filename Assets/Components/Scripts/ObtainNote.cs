using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainNote : MonoBehaviour
{

    public int NoteID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetNote();
        }
    }

    
    public void GetNote()
    {
        Destroy(gameObject);
        NoteManager.instance.obtainedNote[NoteID] = true;

    }
}
