using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicController : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] Space space = Space.World;

    void Update()
    {
        Vector3 direction = Vector3.zero;
        float rotation = 180;
        if (space == Space.World) direction.x = Input.GetAxis("Horizontal");
        else if (space == Space.Self)
        {
            rotation = Input.GetAxis("Horizontal");
        }
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical"); // z is forward/backward vector

        direction = Vector3.ClampMagnitude(direction, 1);


        transform.rotation = Quaternion.Euler(0, rotation * speed, 0); // rotate the object // quaternion is a way to represent rotation in 3D space // euler angles are a way to represent rotation in 3D space
        transform.Translate(direction * speed * Time.deltaTime * speed, space); // move the object // Time.deltaTime is the time it took to complete the last frame // speed is the speed of the object // direction is the direction of the object // space is the space in which the object is moving

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
