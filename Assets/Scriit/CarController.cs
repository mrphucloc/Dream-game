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
    public struct Wheel
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
}
