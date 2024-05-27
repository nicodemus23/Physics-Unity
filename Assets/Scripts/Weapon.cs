using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    [SerializeField] Transform emission;
    [SerializeField] AudioSource? audioSource = null;
    //[SerializeField] Transform view;
    [SerializeField] Camera characterCamera;



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (audioSource != null)
            {
                audioSource?.Play();

            }
            else
            {
                Debug.LogWarning("Audiosource not assigned, dumbass!");
            }
            // create a new instance of the ammo object at the emission position and rotation
            //GameObject ammoInstance = Instantiate(ammo, emission.position, Quaternion.LookRotation(view.forward));
            GameObject ammoInstance = Instantiate(ammo, emission.position, Quaternion.identity);

            // Set the rotation of the ammo instance to match the character's camera direction
            ammoInstance.transform.rotation = Quaternion.LookRotation(characterCamera.transform.forward);
        }
    }

}

//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
 
//public class Weapon : MonoBehaviour
//{
//    [SerializeField] GameObject ammo;
//    [SerializeField] Transform emission;
//    [SerializeField] AudioSource audioSource;

//    public bool equipped = false;
//    void Update()
//    {
//        Debug.DrawRay(emission.position, emission.forward * 10, Color.red);

//        if (equipped && Input.GetMouseButtonDown(0))
//        {
//            if (audioSource != null) audioSource.Play();
//            Instantiate(ammo, emission.position, emission.rotation);
//        }
//    }
//}