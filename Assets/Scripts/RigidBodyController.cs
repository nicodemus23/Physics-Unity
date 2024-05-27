using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyController : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] Space space = Space.World;

    Rigidbody rb;
    Vector3 force = Vector3.zero;
    Vector3 torque = Vector3.zero;
    ForceMode forceMode = ForceMode.Force;
    ForceMode torqueMode = ForceMode.Force;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        float rotation = 0;
        if (space == Space.World) direction.x = Input.GetAxis("Horizontal");
        else if (space == Space.Self)
        {
            rotation = Input.GetAxis("Horizontal");
        }
       // direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical"); // z is forward/backward vector

        direction = Vector3.ClampMagnitude(direction, 1);

        //force = transform.rotation * direction * speed;  
        force = direction * speed;  
        torque = Vector3.up * rotation * speed; // Vector3.up is the same as Vector3(0, 1, 0)


       // transform.rotation = Quaternion.Euler(0, rotation * speed, 0); // rotate the object // quaternion is a way to represent rotation in 3D space // euler angles are a way to represent rotation in 3D space
       // transform.Translate(direction * speed * Time.deltaTime * speed, space); // move the object // Time.deltaTime is the time it took to complete the last frame // speed is the speed of the object // direction is the direction of the object // space is the space in which the object is moving

    }

    private void FixedUpdate()
    {
       // rb.AddForce(force, forceMode);
        rb.AddRelativeForce(force, forceMode);
        rb.AddTorque(torque, torqueMode);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
