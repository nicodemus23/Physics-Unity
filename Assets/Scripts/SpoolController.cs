using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpoolController : MonoBehaviour
{
    public float windSpeed = 10f;
    public float unwindSpeed = 5f;
    public KeyCode windKey = KeyCode.E;
    public KeyCode unwindKey = KeyCode.Q;

    private Rigidbody rb;
    private bool isWinding = false;
    private bool isUnwinding = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check for player input to start winding or unwinding
        if (Input.GetKeyDown(windKey))
        {
            isWinding = true;
        }
        else if (Input.GetKeyUp(windKey))
        {
            isWinding = false;
        }

        if (Input.GetKeyDown(unwindKey))
        {
            isUnwinding = true;
        }
        else if (Input.GetKeyUp(unwindKey))
        {
            isUnwinding = false;
        }
    }

    private void FixedUpdate()
    {
        // Apply rotation to the spool based on winding or unwinding
        if (isWinding)
        {
            // Rotate the spool along the X-axis to wind the rope
            rb.AddTorque(Vector3.right * windSpeed);
        }
        else if (isUnwinding)
        {
            // Rotate the spool along the X-axis to unwind the rope
            rb.AddTorque(Vector3.left * unwindSpeed);
        }
    }
}