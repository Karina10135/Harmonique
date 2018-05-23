using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoleDoor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GM.SceneChange("MoleCave");

        }
    }
}
