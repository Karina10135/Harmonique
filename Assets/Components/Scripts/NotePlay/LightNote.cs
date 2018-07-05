using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNote : MonoBehaviour
{
    public GameObject lightObject;
    public bool on;
    SphereCollider colTrigger;

    private void Start()
    {
        colTrigger = GetComponent<SphereCollider>();
    }

    public void LightTrigger(bool l)
    {
        colTrigger.enabled = l;
        lightObject.gameObject.SetActive(l);
        on = l;
    }

    public void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (on)
        {
            if (other.gameObject.CompareTag("Jellyfish"))
            {

                if (other.GetComponent<Jellyfish>().lighted == false)
                {
                    other.GetComponent<Jellyfish>().TriggerJellyfish();

                }
            }
        }
    }

}
