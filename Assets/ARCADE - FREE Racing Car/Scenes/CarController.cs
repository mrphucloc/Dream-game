using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace;
public class CarController : MonoBehaviour
{
    public CarController OtherCar;

    public enum WheelType
    {
        FrontLeft,
        FrontRight,
        RearLeft,
        RearRight
    }
    [SerializeField]
  
    public class Wheel
    {
        public WheelCollider WheelCollider;
        public Transform WheelTransform;
        public WheelType WheelType;
        public float MaxSteerAngle;
        public float MotorTorque;
    }
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float brakeForce = 50f;

    private float currentSpeed = 0f;

    private void Update()
    {
        HandleInput();
        UpdareWheels();
    }

    private void UpdareWheels()
    {
       
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        foreach (var wheel in wheels) { 
            wheel.WheelCollider.steerAngle = horizontalInput * wheel.MaxSteerAngle;
            float speed = verticalInput* wheel.MotorTorque* Time.deltaTime * acceleration;
            currentSpeed = currentSpeed + speed;
            currentSpeed = Mathf.Clamp(currentSpeed, -10f, maxSpeed);
            wheel.WheelCollider.motorTorque = verticalInput * wheel.MotorTorque;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = brakeForce;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = 0f;
            }
        }
    }
    private void UpdateWheels()
        {
        foreach (var wheel in wheels)
        {
            float rotationAngle = currentSpeed * Time.deltaTime;
            wheel.WheelTransform.Rotate(Vector3.right, rotationAngle);

            Vector3 position;
            Quaternion rotation;
            wheel.WheelCollider.GetWorldPose(out position, out rotation);
            wheel.WheelTransform.position = position;
            wheel.WheelTransform.rotation = rotation;
        }
    }
    }
