using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [SerializeField] private bool isCustomOffset;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float smoothSpeed = 0.1f;

    private void Start()
    {
        // You can also specify your own offset from inspector
        // by making isCustomOffset bool to true
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
        targetPos, smoothSpeed);

        transform.position = smoothFollow;
        //transform.LookAt(target);
    }
}
