using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNote : MonoBehaviour
{
    public GameObject lightObject;
    public bool on;

    public void LightTrigger(bool l)
    {
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
