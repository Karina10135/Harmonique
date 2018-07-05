using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{

    public float lightTimer;
    public ParticleSystem jellyfishLight;
    public GameObject[] jellyfishBody;
    public Material jellyfishStandardMaterial;
    public Material jellyfishLightMaterial;

    public bool lighted;

    private void Start()
    {
        
    }

    public void TriggerJellyfish()
    {
        StartCoroutine(LightTimer());

    }

    IEnumerator LightTimer()
    {
        lighted = true;
        foreach(var body in jellyfishBody)
        {
            body.GetComponent<MeshRenderer>().material = jellyfishLightMaterial;
        }
        var particle = Instantiate(jellyfishLight, gameObject.transform);
        yield return new WaitForSeconds(lightTimer);
        lighted = false;
        foreach (var body in jellyfishBody)
        {
            body.GetComponent<MeshRenderer>().material = jellyfishStandardMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lighted)
        {
            if (other.gameObject.CompareTag("Jellyfish"))
            {
                other.gameObject.GetComponent<Jellyfish>().TriggerJellyfish();
            }
        }
        
    }



}
