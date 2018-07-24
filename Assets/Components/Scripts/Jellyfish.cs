using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{

    public float lightTimer;
    public float distanceFromGround;
    public ParticleSystem jellyfishLight;
    public GameObject[] jellyfishBody;
    public Material jellyfishStandardMaterial;
    public Material jellyfishLightMaterial;
    public bool lighted;

    SphereCollider colTrigger;


    private void Start()
    {
        colTrigger = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.up, Color.magenta);
        //RaycastHit hit;
        //Ray downRay =  new Ray(transform.position, -Vector3.up)
        //if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        //{

        //}

    }

    public void TriggerJellyfish()
    {
        StartCoroutine(LightTimer());

    }

    IEnumerator LightTimer()
    {
        lighted = true;
        colTrigger.enabled = true;
        foreach(var body in jellyfishBody)
        {
            body.GetComponent<MeshRenderer>().material = jellyfishLightMaterial;
        }
        var particle = Instantiate(jellyfishLight, gameObject.transform);
        yield return new WaitForSeconds(lightTimer);
        lighted = false;
        colTrigger.enabled = false;
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

    private void OnTriggerStay(Collider other)
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
