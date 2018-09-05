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
        var blast = Instantiate(ParticleVFX, transform);
        blast.transform.SetParent(null);
        blast.Play();
        Fabric.EventManager.Instance.PostEvent("Misc/NoteCollect", gameObject);
        if (prop != null)
        {
            Destroy(prop);
        }
        Destroy(gameObject);
        NoteManager.instance.NoteAvailable(NoteID);

    }
}
