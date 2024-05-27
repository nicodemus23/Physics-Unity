using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] KeyCode spawnerKey = KeyCode.P;
    

    void Update()
    {
        // check if the P key is pressed
        if (Input.GetKeyDown(spawnerKey))
        {
            // create new instance of the prefab at the spawner's position
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
