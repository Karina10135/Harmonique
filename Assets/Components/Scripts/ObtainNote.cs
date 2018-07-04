using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainNote : MonoBehaviour
{

    public int NoteID;
    public GameObject prop;
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
        blast.Play();
        blast.transform.SetParent(null);
        if (prop != null)
        {
            Destroy(prop);
        }
        Destroy(gameObject);
        NoteManager.instance.NoteAvailable(NoteID);

    }
}
