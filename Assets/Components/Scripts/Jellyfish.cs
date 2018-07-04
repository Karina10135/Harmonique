using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{

    public float lightTimer;
    public GameObject jellyfishLight;
    //public Material jellyfishLightMaterial;
    //public Material jellyfishStandardMaterial;
    public bool lighted;

    private void Start()
    {
        
    }

    public void TriggerJellyfish()
    {
        jellyfishLight.SetActive(true);
        if (lighted)
        {
            StopAllCoroutines();
            StartCoroutine(LightTimer());
        }
        else
        {
            StartCoroutine(LightTimer());
        }
    }

    IEnumerator LightTimer()
    {
        lighted = true;
        yield return new WaitForSeconds(lightTimer);
        jellyfishLight.SetActive(false);
        lighted = false;
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
