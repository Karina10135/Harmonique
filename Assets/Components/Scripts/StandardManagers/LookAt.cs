using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    private void Update()
    {
        //Vector3 pos = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        //transform.LookAt(pos);
        transform.LookAt(Camera.main.transform);
    }
}
