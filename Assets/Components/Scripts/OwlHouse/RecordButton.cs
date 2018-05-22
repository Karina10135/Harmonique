﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{

    public OwlHouse house;

    private void Start()
    {
        house = PuzzleManager.instance.owlHouse;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            house.RecordButton();
        }

    }
}
