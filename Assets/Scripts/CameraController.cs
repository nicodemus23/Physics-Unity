using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the ball transform
    public float distance = 5f; // Distance between the camera and the ball
    public float height = 2f; // Height of the camera above the ball
    public float horizontalSensitivity = 2f; // Sensitivity for horizontal rotation
    public float verticalSensitivity = 2f; // Sensitivity for vertical rotation
    public float smoothSpeed = 0.5f; // Smoothing factor for camera movement

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    private void LateUpdate()
    {
        // Get the mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;

        // Update the rotation angles based on mouse input
        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        // Clamp the vertical rotation to avoid flipping the camera
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

        // Calculate the desired camera position and rotation
        Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
        Vector3 position = target.position - rotation * Vector3.forward * distance + Vector3.up * height;

        // Smoothly interpolate the camera's position and rotation
        transform.position = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smoothSpeed);

        // Adjust the camera's LookAt position to be slightly above the ball
        Vector3 lookAtPosition = target.position + Vector3.up * (height * 0.5f);
        transform.LookAt(lookAtPosition);
    }

    private void OnCollisionStay(Collision collision)
    {
        int layerMask = LayerMask.GetMask("Halfpipe"); 
        if (collision.gameObject.layer == layerMask)
        {
            // Get the closest point on the halfpipe surface to the camera
            Vector3 closestPoint = collision.contacts[0].point;

            // Calculate the new camera position by moving it away from the closest point
            Vector3 newCameraPosition = closestPoint + (transform.position - closestPoint).normalized * distance;

            // Update the camera position
            transform.position = newCameraPosition;
        }
    }
}

