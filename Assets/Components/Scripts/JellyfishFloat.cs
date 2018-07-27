using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishFloat : MonoBehaviour
{

    public float horizontalSpeed;
    public float verticalSpeed;
    public float amplitude;


    public Vector3 tempPosition;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        tempPosition = startPosition;
    }

    private void FixedUpdate()
    {
        tempPosition = transform.position;
        tempPosition.x += horizontalSpeed;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude + startPosition.y;
        transform.position = tempPosition;
    }
}
