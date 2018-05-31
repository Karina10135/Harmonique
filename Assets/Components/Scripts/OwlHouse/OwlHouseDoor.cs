using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlHouseDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PuzzleManager.instance.ChangePlayer(1);
        }
    }
}
