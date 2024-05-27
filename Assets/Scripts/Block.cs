using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    [SerializeField] int points = 100;
    [SerializeField] AudioSource? audioSource = null;

    Rigidbody rb;
    bool destroyed = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > 2 || rb.angularVelocity.magnitude > 2)
        {
            if (audioSource != null)
            {
                audioSource?.Play();

            }
            else
            {
                Debug.LogWarning("Audiosource not assigned, dumbass!");
            }
        }
    }

    // when the block collides with the kill zone destroy the block and add points to the score 
    private void OnTriggerStay(Collider other)
    {
        

        // if the block is touching the kill zone and is not moving destroy the block
        if (!destroyed && other.CompareTag("Kill") && rb.velocity.magnitude == 0 && rb.angularVelocity.magnitude == 0)
        {
            destroyed = true;
            print(points);
            Destroy(gameObject, 2);
        }
    }

    void Update()
    {
        
    }
}
