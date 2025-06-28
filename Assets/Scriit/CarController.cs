using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class CarController : MonoBehaviour
    {
        public enum WheelType
        {
            FrontLeft,
            FrontRight,
            RearLeft,
            RearRight
        }

        [Serializable]
        public struct Wheel
        {
            public Transform wheellTransform;
            public WheelCollider wheelColliider;
            public WheelType wheelType;
            public float MaxSteerAngle;
            public float MotorTorque;
        }

        [SerializeField] private List<Wheel> wheelss;

        [SerializeField] private float MaxSpeed = 100f;
        [SerializeField] private float acceleratiion = 10f;
        [SerializeField] private float BrakeForce = 50f;

        private float currenttSpeed = 0f;

        private void Update()
        {
            this.HandleeInput();
            this.UpdateWheeels();
        }

        private void UpdateWheeels()
        {
            foreach (var wheel in wheelss)
            {
                // Update wheel rotation
                float rotationAngle = currenttSpeed * Time.deltaTime;
                wheel.wheellTransform.Rotate(Vector3.right, rotationAngle);

                // Update wheel position
                Vector3 position;
                Quaternion rotation;
                wheel.wheelColliider.GetWorldPose(out position, out rotation);
                wheel.wheellTransform.position = position;
                wheel.wheellTransform.rotation = rotation;
            }
        }

        private void HandleeInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Steer front wheels
            foreach (var wheel in wheelss)
            {
                wheel.wheelColliider.steerAngle = horizontalInput * wheel.MaxSteerAngle;
                float speed = verticalInput * wheel.MotorTorque * Time.deltaTime * acceleratiion;
                currenttSpeed += speed;
                currenttSpeed = Mathf.Clamp(currenttSpeed, -10f, MaxSpeed);
                wheel.wheelColliider.motorTorque = currenttSpeed;
            }
            // Apply brake force
            if (Input.GetKey(KeyCode.Space))
            {
                foreach (var wheel in wheelss)
                {
                    wheel.wheelColliider.brakeTorque = BrakeForce;
                }
            }
            else
            {
                foreach (var wheel in wheelss)
                {
                    wheel.wheelColliider.brakeTorque = 0f;
                }
            }
        }
    }
}