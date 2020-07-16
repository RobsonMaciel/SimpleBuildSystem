using System.Collections.Generic;
using UnityEngine;

public class ConstructionParts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject peace;
    public string name;
    public string tagName;
    public List<GameObject> meshs;
    public List<GameObject> sockets;

    public GameObject Peace
    {
        get => peace;
        private set => peace = value;
    }

    public string Name
    {
        get => name;
        private set => name = value;
    }

    public string TagName
    {
        get => tagName;
        private set => tagName = value;
    }
}