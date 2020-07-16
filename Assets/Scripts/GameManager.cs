using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject blocs;
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
                    Instantiate(blocs, new Vector3(i, 0, j), transform.rotation);
                }
            }
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            builder.selectedObject = null;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            builder.selectedObject = objectToInstance[0];
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            builder.selectedObject = objectToInstance[1];
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            builder.selectedObject = objectToInstance[2];
        }
    }
}