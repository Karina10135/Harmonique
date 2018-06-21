using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstNote : MonoBehaviour
{
    //[HideInInspector]
    public GameObject player;
    public ParticleSystem blow;
    public float power;
    public float radius;
    public float upForce;

    public void Start()
    {
        player = GetComponent<NoteManager>().player;
    }


    public void Detonate()
    {
        Vector3 expPos = player.transform.position;
        Collider[] colliders = Physics.OverlapSphere(expPos, radius);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null && rb.gameObject.CompareTag("Leaf"))
            {
                rb.AddExplosionForce(power, expPos, radius, upForce, ForceMode.Impulse);

            }
        }
        blow.Play();

    }

}
