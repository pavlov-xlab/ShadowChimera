using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShadowChimera
{
    public class CarController : MonoBehaviour
    {
        [Serializable] 
        public class Wheel
        {
            [SerializeField] private Transform wheelTransform;
            [SerializeField] private WheelCollider wheelCollider;
            private Quaternion m_steerlessLocalRotation;
            
            public float motorTorque
            {
                set => wheelCollider.motorTorque = value;
                get => wheelCollider.motorTorque;
            }
            
            public float brakeTorque
            {
                set => wheelCollider.brakeTorque = value;
                get => wheelCollider.brakeTorque;
            }
            
            public float steerAngle
            {
                set => wheelCollider.steerAngle = value;
                get => wheelCollider.steerAngle;
            }
            
            public void UpdateWheelFromCollider()
            {
                wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
                wheelTransform.position = position;
                wheelTransform.rotation = rotation;
            }
            
            public void Setup() => m_steerlessLocalRotation = wheelTransform.localRotation;
            public void StoreDefaultRotation() => m_steerlessLocalRotation = wheelTransform.localRotation;
            public void SetToDefaultRotation() => wheelTransform.localRotation = m_steerlessLocalRotation;
        }
        
        
        public Wheel frontLeftWheel;
        public Wheel frontRightWheel;
        public Wheel rearLeftWheel;
        public Wheel rearRightWheel;

        public float accelerationStats = 1000f;
        public float brakingStats = 1000f;
        public float steeringAnimationDamping = 10f;
        public float maxSteeringAngle = 30f;
        
        private float m_smoothedSteeringInput = 0f;
        private float m_currentAcceleration = 0.0f;
        private float m_currentBreakForce = 0.0f;
        private int m_gear = 0;
        private float m_lastBrakeValue = 0f;
        private Rigidbody m_rigidbody;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            
            frontLeftWheel.Setup();
            frontRightWheel.Setup();
            rearLeftWheel.Setup();
            rearRightWheel.Setup();
        }

        private void Move()
        {
            // apply acceleration => All wheelss
            rearLeftWheel.motorTorque = m_currentAcceleration;
            rearRightWheel.motorTorque = m_currentAcceleration;
            // FrontLeftWheel.motorTorque = m_currentAcceleration;
            // FrontRightWheel.motorTorque = m_currentAcceleration;

            // break force => all wheels
            rearLeftWheel.brakeTorque = m_currentBreakForce;
            rearRightWheel.brakeTorque = m_currentBreakForce;
            frontLeftWheel.brakeTorque = m_currentBreakForce;
            frontRightWheel.brakeTorque = m_currentBreakForce;
        }
    
        
        private void InputControl()
        {
            var accel = Keyboard.current.upArrowKey.isPressed ? 1f : 0f;
            var brake = Keyboard.current.downArrowKey.isPressed ? 1f : 0f;
            // var accel = 0f;
            // var brake = 0f;
            CheckAutoReverse(ref accel, ref brake, ref m_gear);
            
            m_currentAcceleration = m_gear == -1 ?  -accel : accel;
            m_currentAcceleration *= accelerationStats;
            
            m_currentBreakForce = m_gear == -1 ?  -brake : brake;
            m_currentBreakForce *= brakingStats;
        }
        
        private void CheckAutoReverse(ref float acceleration, ref float brake, ref int gear)
        {
            var velocity = m_rigidbody.velocity;
            var forward = transform.forward;
			
            if (Vector3.Dot(forward, velocity) < 0f || velocity.sqrMagnitude < (0.1f * 0.1f))
            {
                if (brake > 0f && acceleration < 0.7f && m_lastBrakeValue < 1f)
                {
                    gear = -1;
                }
			
                if (acceleration > 0.1f && gear < 0)
                {
                    gear = 1;
                }
            }
			
            m_lastBrakeValue = brake;
			
            if (gear != -1)
            {
                return;
            }
            (acceleration, brake) = (brake, acceleration);
        }

        private void Update()
        {
            InputControl();
        }

        void FixedUpdate() 
        {
            m_smoothedSteeringInput = Mathf.MoveTowards(m_smoothedSteeringInput, Input.GetAxis("Horizontal"), 
                steeringAnimationDamping * Time.deltaTime);

            // Steer front wheels
            float rotationAngle = m_smoothedSteeringInput * maxSteeringAngle;

            frontLeftWheel.steerAngle = rotationAngle;
            frontRightWheel.steerAngle = rotationAngle;
            
            Move();
        }
        
        void LateUpdate()
        {
            // Update position and rotation from WheelCollider
            frontLeftWheel.UpdateWheelFromCollider();
            frontRightWheel.UpdateWheelFromCollider();
            rearLeftWheel.UpdateWheelFromCollider();
            rearRightWheel.UpdateWheelFromCollider();
        }
    }
}
