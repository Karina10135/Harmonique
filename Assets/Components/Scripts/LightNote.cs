﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNote : MonoBehaviour
{
    public Light light;
    public bool on;

    public void LightTrigger(bool l)
    {

        light.gameObject.SetActive(l);
    }

}
