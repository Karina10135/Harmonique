﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Mole Guard")
        {
            NoteManager.instance.InteractableObject(other.gameObject.tag);
            print("guard");
        }

    }
}
