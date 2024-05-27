using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float downwardForceMagnitude = 10.0f;
    [SerializeField] float forwardForceMultiplier = 2.0f;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ApplyInitialForces();
    }

    private void ApplyInitialForces()
    {
        // apply a downward force to the ball
        Vector3 downwardForce = -transform.up * downwardForceMagnitude;
        rb.AddForce(downwardForce, ForceMode.Impulse);

        // apply a forward force to the ball
        Vector3 forwardForce = transform.forward * forwardForceMultiplier;
        rb.AddForce(forwardForce, ForceMode.Impulse);


    }
}