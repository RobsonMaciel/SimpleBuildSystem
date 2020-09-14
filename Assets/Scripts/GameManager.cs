using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
            setActiveFalse();
            objectToInstance[0].SetActive(true);
            builder.selectedObject = objectToInstance[0];
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            setActiveFalse();
            objectToInstance[1].SetActive(true);
            builder.selectedObject = objectToInstance[1];
        }
        //
        // if (Input.GetKey(KeyCode.Alpha3))
        // {
        //     setActiveFalse();
        //     builder.selectedObject = objectToInstance[2];
        // }
    }

    void setActiveFalse()
    {
        foreach (GameObject o in objectToInstance)
        {
            o.SetActive(false);
        }
    }
}