using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGuard : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }

    void Pass()
    {
        print("Enter");
    }

    void Denied()
    {
        print("No Entry");
    }

}
