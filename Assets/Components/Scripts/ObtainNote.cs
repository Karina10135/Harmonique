using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainNote : MonoBehaviour
{

    public int NoteID;
    public ParticleSystem ParticleVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetNote();
        }
    }

    
    public void GetNote()
    {
        //Instantiate(ParticleVFX, gameObject.transform);
        Destroy(gameObject);
        NoteManager.instance.NoteAvailable(NoteID);

    }
}
