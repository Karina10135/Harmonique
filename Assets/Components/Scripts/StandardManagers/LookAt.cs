﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target;
    public float damping = 10f;

    private void Start()
    {
        target = Camera.main.gameObject;
    }

    private void Update()
    {
        //Vector3 pos = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        //transform.LookAt(pos);
        //transform.LookAt(Camera.main.transform);
        
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
