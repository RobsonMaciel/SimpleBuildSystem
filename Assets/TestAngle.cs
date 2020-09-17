using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAngle : MonoBehaviour
{
    public float rotationSpeed = 45;
    public Vector3 currentEulerAngles;
    public float x;
    public float y;
    public float z;
    public Vector3 deltaTime;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) x = 1 - x;
        if (Input.GetKeyDown(KeyCode.Y)) y = 1 - y;
        if (Input.GetKeyDown(KeyCode.Z)) z = 1 - z;

        //modifying the Vector3, based on input multiplied by speed and time
        deltaTime = new Vector3(x, y, z) * Time.deltaTime;
        
        currentEulerAngles += deltaTime * rotationSpeed;

        //apply the change to the gameObject
        transform.eulerAngles = currentEulerAngles;
    }
}