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
        var blast = Instantiate(ParticleVFX, gameObject.transform);
        blast.transform.SetParent(null);
        Destroy(gameObject);
        NoteManager.instance.NoteAvailable(NoteID);

    }
}
