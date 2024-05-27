using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarController : MonoBehaviour
{
    [System.Serializable]
    public struct Wheel
    {
        public WheelCollider collider;
        public Transform transform;
    }
    [System.Serializable]
    public struct Axle
    {
        public Wheel leftWheel;
        public Wheel rightWheel;
        public bool isMotor;
        public bool isSteering;
    }
    [SerializeField] Axle[] axles;
    [SerializeField] float maxMotorTorque;
    [SerializeField] float maxSteeringAngle;
    public void FixedUpdate()
    {
        float motor = Input.GetAxis("Vertical") * maxMotorTorque;
        float steering = Input.GetAxis("Horizontal") * maxSteeringAngle;
    foreach (Axle axle in axles)
        {
            if (axle.isSteering)
            {
                axle.leftWheel.collider.steerAngle = steering;
                axle.rightWheel.collider.steerAngle = steering;
                UpdateWheelTransform(axle.leftWheel);
                UpdateWheelTransform(axle.rightWheel);
    
            }
            if (axle.isMotor)
            {
                axle.leftWheel.collider.motorTorque = motor;
                axle.rightWheel.collider.motorTorque = motor;

            }
        }
    }

    public void UpdateWheelTransform(Wheel wheel)
    {
        wheel.collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheel.transform.position = position;
        wheel.transform.rotation = rotation;
        //Vector3 forward = wheel.transform.position - wheel.collider.attachedRigidbody.transform.position;
        //wheel.transform.rotation = Quaternion.LookRotation(forward, wheel.collider.transform.up);
    }

}

