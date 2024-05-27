using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammo : MonoBehaviour
{
    [SerializeField] float speed = 500f; // Increase the speed value significantly
    [SerializeField] float lifeSpan = 0;
    [SerializeField] float mass = 100f; // Adjust the mass of the ammo object

    Rigidbody rb;

    void Start()
    {
        // only destroy the object if the lifeSpan is greater than 0
        if (lifeSpan > 0) Destroy(gameObject, lifeSpan);

        rb = GetComponent<Rigidbody>();
        rb.mass = mass; // Set the mass of the ammo object

        // add force to the object in the forward direction
        rb.AddRelativeForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    void Update()
    {
    }
}