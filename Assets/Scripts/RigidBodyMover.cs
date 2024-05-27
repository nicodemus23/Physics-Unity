using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class RigidBodyMover : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] ForceMode mode;
    [SerializeField] Vector3 torque; // rotation force
    [SerializeField] ForceMode torqueMode; // rotation force mode (torque)


    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(force, mode); // adds force to the rigidbody in direction of the force vector
            rb.AddTorque(torque, torqueMode); // adds torque to the rigidbody in direction of the torque vector
        }
    }
}
