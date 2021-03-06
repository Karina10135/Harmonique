﻿using UnityEngine;
using System.Collections;

public static class PlayerInput
{
    private static float lookAngle = 0f;
    private static float tiltAngle = 0f;

    private static bool move = true;

    public static Vector3 GetMovementInput(Camera relativeCamera)
    {
        Vector3 moveVector;
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (relativeCamera != null)
        {
            // Calculate the move vector relative to camera rotation
            Vector3 scalerVector = new Vector3(1f, 0f, 1f);
            Vector3 cameraForward = Vector3.Scale(relativeCamera.transform.forward, scalerVector).normalized;
            Vector3 cameraRight = Vector3.Scale(relativeCamera.transform.right, scalerVector).normalized;

            moveVector = (cameraForward * verticalAxis + cameraRight * horizontalAxis);

        }
        else
        {
            // Use world relative directions
            moveVector = (Vector3.forward * verticalAxis + Vector3.right * horizontalAxis);
        }

        if (moveVector.magnitude > 1f)
        {
            moveVector.Normalize();
        }

        return moveVector;
    }

    public static Quaternion GetMouseRotationInput(float mouseSensitivity = 2f, float minTiltAngle = -75f, float maxTiltAngle = 45f)
    {
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (Input.GetMouseButtonDown(1))
        {
            move = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            move = true;
        }

        if (!move)
        {
            mouseX = 0;
            mouseY = 0;
        }


        // Adjust the look angle (Y Rotation)
        lookAngle += mouseX * mouseSensitivity;
        lookAngle %= 360f;

        // Adjust the tilt angle (X Rotation)
        tiltAngle += mouseY * mouseSensitivity;
        tiltAngle %= 360f;
        tiltAngle = MathfExtensions.ClampAngle(tiltAngle, minTiltAngle, maxTiltAngle);

        var controlRotation = Quaternion.Euler(/*20f*/-tiltAngle, lookAngle, 0f);
        return controlRotation;
    }

    public static bool GetSprintInput()
    {
        return Input.GetButton("Sprint");
    }

    public static bool GetJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public static bool GetToggleWalkInput()
    {
        return Input.GetButtonDown("Toggle Walk");
    }
}
