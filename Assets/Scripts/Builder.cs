using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public int layerMask;
    public GameObject selectedObject;
    public RaycastHit hit;
    public bool snapped;
    public Vector3 oldPosition;

    private const string SocketName = "Socket";
    private const int DistanceOfRayCast = 5;

    void FixedUpdate()
    {
        layerMask = 1 << 7;
        layerMask = ~layerMask;

        if (null != selectedObject)
        {
            ConstructionParts constructionParts = selectedObject.GetComponent<ConstructionParts>();

            manipulateMeshsOrSockets(constructionParts.sockets, false, false);
            manipulateMeshsOrSockets(constructionParts.meshs, false, false);

            RayCastCreator(constructionParts);

            if (Input.GetMouseButtonDown(0) && !checkColliders() && snapped)
            {
                instantiateObjectInTheScene();
            }
        }
    }

    private void RayCastCreator(ConstructionParts constructionParts)
    {
        oldPosition = selectedObject.transform.position;
        var normalizedPosition = normalizedPositionToRayCast();

        if (Physics.Raycast(normalizedPosition, transform.TransformDirection(Vector3.forward), out hit,
            Mathf.Infinity, layerMask) && hit.distance <= DistanceOfRayCast && isMineTag(hit.transform))
        {
            Vector3 transformPosition = hit.transform.GetChild(0).transform.position;
            selectedObject.transform.position =
                Movegameobjectfrompointatopointbsmoothly(oldPosition, transformPosition, 20);
            holdObject(selectedObject, hit.transform.rotation);
            snapped = true;
            Debug_DrawLine(normalizedPosition, hit.point, Color.green);
        }
        else
        {
            Vector3 distanceOfRayCast = transform.position + transform.forward * DistanceOfRayCast;
            selectedObject.transform.position =
                Movegameobjectfrompointatopointbsmoothly(oldPosition, distanceOfRayCast, 20);
            holdObject(selectedObject, new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w));
            snapped = constructionParts.IsFundamentalParts;
            Debug_DrawLine(normalizedPosition, selectedObject.transform.position, Color.red);
        }
    }

    private Vector3 Movegameobjectfrompointatopointbsmoothly(Vector3 pointA, Vector3 pointB, int speedToSnap)
    {
        float step = speedToSnap * Time.deltaTime;
        return Vector3.MoveTowards(pointA, pointB, step);
    }

    private void instantiateObjectInTheScene()
    {
        GameObject instantiate = Instantiate(selectedObject, selectedObject.transform.position,
            selectedObject.transform.rotation);

        instantiate.name = selectedObject.name;
        MeshRenderer[] componentsInChildren = instantiate.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in componentsInChildren)
        {
            meshRenderer.materials[0].color = Color.white;
            StandardShaderUtils.changeRenderMode(meshRenderer.materials[0],
                StandardShaderUtils.BlendMode.opaque, Color.white);
            OverlapBoxCollider overlap = meshRenderer.GetComponent<OverlapBoxCollider>();
            Destroy(overlap);
        }

        manipulateMeshsOrSockets(instantiate.GetComponent<ConstructionParts>().sockets, true, true);
        manipulateMeshsOrSockets(instantiate.GetComponent<ConstructionParts>().meshs, true, false);
    }

    private Vector3 normalizedPositionToRayCast()
    {
        Vector3 transformForwardNormalized =
            new Vector3(transform.forward.x * 1.5f, transform.forward.y, transform.forward.z * 1.5f);

        Vector3 normalizedPosition = transform.position + transformForwardNormalized;
        return normalizedPosition;
    }

    private static void Debug_DrawLine(Vector3 position, Vector3 hitPoint, Color color)
    {
        Debug.DrawLine(position, hitPoint, color);
    }

    private void manipulateMeshsOrSockets(List<GameObject> gameObjects, bool isEnableBoxCollider,
        bool isTriggerBoxCollider)
    {
        foreach (GameObject o in gameObjects)
        {
            BoxCollider[] componentsInChildren = o.GetComponentsInChildren<BoxCollider>();

            foreach (BoxCollider componentsInChild in componentsInChildren)
            {
                componentsInChild.enabled = isEnableBoxCollider;
                componentsInChild.isTrigger = isTriggerBoxCollider;
            }
        }
    }

    private bool isMineTag(Transform transform)
    {
        string transformTag = transform.tag;

        if (!transformTag.Substring(0, 6).Equals(SocketName))
        {
            return false;
        }

        return transformTag.Substring(7, transformTag.Length - 7)
            .Equals(selectedObject.GetComponentInParent<ConstructionParts>().TagName);
    }

    private bool checkColliders()
    {
        OverlapBoxCollider[] componentsInChildren = selectedObject.GetComponentsInChildren<OverlapBoxCollider>();

        foreach (var boxCollider in componentsInChildren)
        {
            if (boxCollider.IsTriggered)
            {
                return true;
            }
        }

        return false;
    }

    private void holdObject(GameObject objectHolded, Quaternion quaternion)
    {
        objectHolded.transform.rotation = quaternion;

        BoxCollider[] componentsInChildren = objectHolded.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in componentsInChildren)
        {
            boxCollider.isTrigger = true;
        }
    }
}