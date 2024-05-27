using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float smoothSpeed = 0.5f;
    public float orbitSpeed = 2f;
    public LayerMask obstacleLayer;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0f, height, distance);
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        Quaternion horizontalRotation = Quaternion.AngleAxis(horizontalInput * orbitSpeed, Vector3.up);
        Quaternion verticalRotation = Quaternion.AngleAxis(-verticalInput * orbitSpeed, transform.right);

        offset = horizontalRotation * verticalRotation * offset;

        Vector3 desiredPosition = target.position + offset;

        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit, obstacleLayer))
        {
            desiredPosition = hit.point;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target);
    }
}