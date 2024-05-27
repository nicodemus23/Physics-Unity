using UnityEngine;

public class SnowBallController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rollSpeed = 100f;
    public Transform cameraTransform; // Reference to the camera's transform

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the camera's rotation
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = cameraTransform.TransformDirection(movement);

        // Apply the movement to the rigidbody
        float movementForce = moveSpeed * Time.deltaTime;
        rb.AddForce(movement * movementForce, ForceMode.Acceleration); // Apply force to move the ball
        //transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World); // Alternative way to move the ball with Translate

        // Apply torque to make the ball roll from the start in the direction of movement
        Vector3 torqueDirection = Vector3.Cross(movement.normalized, Vector3.up);
        rb.AddTorque(torqueDirection * rollSpeed * Time.deltaTime);

        // check if the ball is inside of the halfpipe
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.0f)) 
        {
            // apply upward force when moving up inside the halfpipe by checking the normal of the hit point
            if (hit.normal.y < 0.9f && moveVertical > 0) // check if the normal of the hit point is less than threshold (half of the normal vector)
            {
                float slopeForce = movementForce * 12.0f;
                rb.AddForce(Vector3.up * slopeForce, ForceMode.Acceleration);
            }
        }

        // apply upward force when moving up inside the halfpipe - this made the ball take flight (unconditional force upward)
        //if (moveVertical > 0)
        //{
        //    float upwardForce = movementForce * 4.0f;
        //    rb.AddForce(Vector3.up * upwardForce, ForceMode.Acceleration);
        //}

        rb.AddForce(Physics.gravity, ForceMode.Acceleration); // Apply gravity
    }
}

//public float moveSpeed = 5f;
//public float rollSpeed = 100f;
//public Transform cameraTransform; // Reference to the camera's transform

//private Rigidbody rb;

//private void Start()
//{
//    rb = GetComponent<Rigidbody>();
//}

//private void Update()
//{
//    float moveHorizontal = Input.GetAxis("Horizontal");
//    float moveVertical = Input.GetAxis("Vertical");

//    // Calculate the movement direction based on the camera's rotation
//    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
//    movement = cameraTransform.TransformDirection(movement);

//    // Apply the movement to the rigidbody
//    transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

//    // Apply torque to make the ball roll from the start in the direction of movement
//    Vector3 torqueDirection = Vector3.Cross(movement.normalized, Vector3.up);
//    rb.AddTorque(torqueDirection * rollSpeed * Time.deltaTime);
//    rb.AddForce(Physics.gravity, ForceMode.Acceleration); // Apply gravity
//}