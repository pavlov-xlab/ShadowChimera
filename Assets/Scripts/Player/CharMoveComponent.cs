using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	public class CharMoveComponent : MonoBehaviour, IMoveComponent
	{
		[SerializeField] private CharacterController m_characterController;
		[SerializeField] private Transform m_cameraTarget;
		[SerializeField] private float m_moveSpeed = 5f;
		[SerializeField] private float m_sprintSpeed = 10f;
		[SerializeField] private float m_rotationSmoothTime = 0.12f;
		[SerializeField] private float m_speedChangeRate = 10f;
		[SerializeField] private float m_jumpHeight = 1.2f;

		private const float TerminalVelocity = 53f;
		private const float VerticalVelocityMin = -2f;

		private float m_rotationVelocity;
		private float m_targetRotation;
		private float m_verticalVelocity;
		private float gravity => Physics.gravity.y;

		public Vector3 velocity => m_characterController.velocity;
		public bool isGrounded => m_characterController.isGrounded;
		
		public event Action onJump;

		private void OnEnable()
		{
			m_characterController.enabled = true;
		}

		private void OnDisable()
		{
			m_characterController.enabled = false;
		}

		public void Init(float speed, float sprintSpeed)
		{
			m_moveSpeed = speed;
			m_sprintSpeed = sprintSpeed;
		}

		public void Move(Vector2 move, bool isSprint, float cameraY)
		{
			float targetSpeed = 0f;
			float speed = 0;
			float inputMagnitude = move.magnitude;

			if (inputMagnitude != 0)
			{
				targetSpeed = isSprint ? m_sprintSpeed : m_moveSpeed;
			}

			var characterVelocity = m_characterController.velocity;
			float currentHorizontalSpeed = new Vector3(characterVelocity.x, 0f, characterVelocity.z).magnitude;

			const float speedOffset = 0.1f;

			if (currentHorizontalSpeed < targetSpeed - speedOffset ||
				currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * m_speedChangeRate);

				// round speed to 3 decimal places
				speed = Mathf.Round(speed * 1000f) / 1000f;
			}
			else
			{
				speed = targetSpeed;
			}

			var targetTr = m_characterController.transform;


			if (inputMagnitude != 0f)
			{
				Vector3 inputDirection = new Vector3(move.x, 0f, move.y).normalized;

				m_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraY;

				float rotation = Mathf.SmoothDampAngle(targetTr.eulerAngles.y, m_targetRotation, ref m_rotationVelocity, m_rotationSmoothTime);

				targetTr.rotation = Quaternion.Euler(0f, rotation, 0f);
			}

			if (m_characterController.isGrounded)
			{
				if (m_verticalVelocity < 0f)
				{
					m_verticalVelocity = VerticalVelocityMin;
				}
			}

			if (m_verticalVelocity < TerminalVelocity)
			{
				m_verticalVelocity += gravity * Time.deltaTime;
			}
			
			Vector3 targetDirection = Quaternion.Euler(0f, m_targetRotation, 0f) * Vector3.forward;
			Vector3 vertical = new Vector3(0f, m_verticalVelocity * Time.deltaTime, 0f);
			Vector3 horizontal = targetDirection.normalized * (speed * Time.deltaTime);
			m_characterController.Move(horizontal + vertical);
		}

		public void Look(Quaternion rotation)
		{
			m_cameraTarget.rotation = rotation;
		}

		public void Jump()
		{
			if (m_characterController.isGrounded)
			{
				m_verticalVelocity = Mathf.Sqrt(m_jumpHeight * VerticalVelocityMin * gravity);
				onJump?.Invoke();
			}
		}
	}
}
