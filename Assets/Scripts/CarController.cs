using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class CarController : MonoBehaviour
    {
        public WheelCollider frontLeftWheel;
        public WheelCollider frontRightWheel;
        public WheelCollider rearLeftWheel;
        public WheelCollider rearRightWheel;

        public float accelerationStats = 1000f;
        public float brakingStats = 1000f;
        
        private float m_currentAcceleration = 0.0f;
        private float m_currentBreakForce = 0.0f;
        private int m_gear = 0;
        private float m_lastBrakeValue = 0f;
        private Rigidbody m_rigidbody;

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
            // var accel = Input.Accelerate ? 1f : 0f;
            // var brake = Input.Brake ? 1f : 0f;
            var accel = 0f;
            var brake = 0f;
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
    }
}
