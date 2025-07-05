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
            public WheelCollider wheellCollider;
            public WheelType wheelType;
            public float MaxSteerAngle;
            public float MotorTorque;
        }

        [SerializeField] private List<Wheel> wheelss;

        [SerializeField] private float MaxSpeed = 100f;
        [SerializeField] private float Acceleration = 10f;
        [SerializeField] private float BrakeForce = 50f;

        [SerializeField] private bool canControl;

        private float currenttSpeed = 0f;

        private void Update()
        {
            this.HandleInputt();
            this.UpdateWheelss();
        }

        private void UpdateWheelss()
        {
            foreach (var wheel in wheelss)
            {
                // Update wheel rotation
                float rotationAngle = currenttSpeed * Time.deltaTime;
                wheel.wheellTransform.Rotate(Vector3.right, rotationAngle);

                // Update wheel position
                Vector3 position;
                Quaternion rotation;
                wheel.wheellCollider.GetWorldPose(out position, out rotation);
                wheel.wheellTransform.position = position;
                wheel.wheellTransform.rotation = rotation;
            }
        }

        public void ApplyInput(float horizontalInput, float verticalInput)
        {
            // Steer front wheels
            foreach (var wheel in wheelss)
            {
                wheel.wheellCollider.steerAngle = horizontalInput * wheel.MaxSteerAngle;
                float speed = verticalInput * wheel.MotorTorque * Time.deltaTime * Acceleration;
                currenttSpeed += speed;
                currenttSpeed = Mathf.Clamp(currenttSpeed, -10f, MaxSpeed);
                wheel.wheellCollider.motorTorque = currenttSpeed;
            }
            // Apply brake force
            if (Input.GetKey(KeyCode.Space))
            {
                foreach (var wheel in wheelss)
                {
                    wheel.wheellCollider.brakeTorque = BrakeForce;
                }
            }
            else
            {
                foreach (var wheel in wheelss)
                {
                    wheel.wheellCollider.brakeTorque = 0f;
                }
            }
        }

        private void HandleInputt()
        {
            if (!canControl) return;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            ApplyInput(horizontalInput, verticalInput);
        }
    }
}