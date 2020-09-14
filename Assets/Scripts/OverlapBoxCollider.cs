using UnityEngine;

public class OverlapBoxCollider : MonoBehaviour
{
    private bool _started;
    private bool triggered;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Collider[] hitColliders;

    public bool IsTriggered
    {
        get => triggered;
        set
        {
            setTransparence(value ? Color.red : Color.green);

            triggered = value;
        }
    }

    void setTransparence(Color color)
    {
        StandardShaderUtils.changeRenderMode(gameObject.transform.GetComponent<MeshRenderer>().materials[0],
            StandardShaderUtils.BlendMode.transparent, color);
    }

    void Start()
    {
        _started = true;
    }

    void FixedUpdate()
    {
        checkCollisions();
    }


    void checkCollisions()
    {
        var transform1 = transform;
        Vector3 transform1LossyScale = transform1.lossyScale / 2;
        Vector3 lossyScalePercent = transform1LossyScale * 0.20f;
        hitColliders = Physics.OverlapBox(transform.position, transform1LossyScale - lossyScalePercent , transform1.rotation.normalized,
            layerMask);
        IsTriggered = hitColliders.Length > 0;
    }

    void OnDrawGizmos()
    {
        if (_started)
        {
            Gizmos.color = Color.black;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, transform.GetComponent<BoxCollider>().size);
        }
    }
}