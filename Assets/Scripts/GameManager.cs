using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject blocks;
    public int X;
    public int Z;
    public bool createBLocks;
    public GameObject[] objectToInstance;
    public Builder builder;

    private void Start()
    {
        if (createBLocks)
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Z; j++)
                {
                    Instantiate(blocks, new Vector3(i, 0, j), transform.rotation);
                }
            }
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            builder.selectedObject = null;
            setActiveFalse();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            SelectBuildParts(0);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SelectBuildParts(1);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
        {
            RotateParts(-45);
        }else if (Input.GetKey(KeyCode.R))
        {
            RotateParts(45);
        }
    }

    public Vector3 currentEulerAngles;
    public float x;
    public float y;
    public float z;
    public Vector3 deltaTime;

    void RotateParts(float rotationSpeed)
    {
        y = 3 - y;
        deltaTime = new Vector3(x, y, z) * Time.deltaTime;
        currentEulerAngles += deltaTime * rotationSpeed;
        builder.selectedObject.transform.eulerAngles = currentEulerAngles;
    }

    private void SelectBuildParts(int indexObject)
    {
        Transform mainCameraTransform = Camera.main.transform;
        Vector3 transformForwardNormalized =
            new Vector3(
                mainCameraTransform.forward.x * 5f,
                mainCameraTransform.forward.y,
                mainCameraTransform.forward.z * 5f
            );

        Vector3 playerPositionYAdjusted = mainCameraTransform.position + transformForwardNormalized;

        setActiveFalse();
        objectToInstance[indexObject].SetActive(true);
        objectToInstance[indexObject].transform.position = playerPositionYAdjusted;
        builder.selectedObject = objectToInstance[indexObject];
    }

    void setActiveFalse()
    {
        foreach (GameObject o in objectToInstance)
        {
            o.SetActive(false);
        }
    }
}