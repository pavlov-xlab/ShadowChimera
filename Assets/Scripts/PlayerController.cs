using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace ShadowChimera
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private Character m_character;
		[SerializeField] private InputActionAsset m_inputActionAsset;
		[SerializeField] private Transform m_cameraTarget;
		[SerializeField] private Transform m_cameraTransform;		
		[SerializeField] private float m_speedRotation = 200f;
		[SerializeField] private float m_topClamp = 70f;
		[SerializeField] private float m_bottomClamp = -9f;


		private float m_cameraTargetYaw;
		private float m_cameraTargetPitch;

		//Input
		private InputActionMap m_playerMap;
		private InputAction m_moveAction;
		private InputAction m_lookAction;
		private InputAction m_fireAction;


		private void Awake()
		{
			m_playerMap = m_inputActionAsset.FindActionMap("Player");
			m_moveAction = m_playerMap.FindAction("Move");
			m_lookAction = m_playerMap.FindAction("Look");
			m_fireAction = m_playerMap.FindAction("Fire");
		}

		private void OnEnable()
		{
			m_playerMap.Enable();

			m_fireAction.performed += OnFireInput;
		}


		private void OnDisable()
		{
			m_playerMap.Disable();

			m_fireAction.performed -= OnFireInput;
		}

		private void OnFireInput(InputAction.CallbackContext context)
		{
			Debug.Log("Try fire!");
		}

		private void Update()
		{
			Vector2 move = m_moveAction.ReadValue<Vector2>();
			Move(move, false);
		}

		private void LateUpdate()
		{
			Vector2 look = m_lookAction.ReadValue<Vector2>();
			CameraRotation(look);
		}

		private void Move(Vector2 move, bool isSprint)
		{
			m_character.Move(move, isSprint, m_cameraTransform.eulerAngles.y);
		}		

		private void CameraRotation(Vector2 look)
		{
			const float threshold = 0.01f;
			
			if (look.sqrMagnitude >= threshold)
			{
				float deltaTimeMultiplier = m_speedRotation * Time.deltaTime;

				m_cameraTargetYaw += look.x * deltaTimeMultiplier;
				m_cameraTargetPitch += look.y * deltaTimeMultiplier;
			}

			m_cameraTargetYaw = ClampAngle(m_cameraTargetYaw, float.MinValue, float.MaxValue);
			m_cameraTargetPitch = ClampAngle(m_cameraTargetPitch, m_bottomClamp, m_topClamp);

			m_character.Look(Quaternion.Euler(m_cameraTargetPitch, m_cameraTargetYaw, 0f));
		}

		private static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}

			if (angle > 360f)
			{
				angle -= 360f;
			}

			return Mathf.Clamp(angle, min, max);
		}
	}
}