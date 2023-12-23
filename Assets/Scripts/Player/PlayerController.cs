using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ShadowChimera
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private Character m_character;
		[SerializeField] private InputActionAsset m_inputActionAsset;
		[SerializeField] private Transform m_cameraTransform;
		[SerializeField] private float m_speedRotation = 200f;
		[SerializeField] private float m_topClamp = 70f;
		[SerializeField] private float m_bottomClamp = -9f;

		private CharMoveComponent m_charMoveController;

		public event System.Action onUseItem;

		public Character character => m_character;


		private float m_cameraTargetYaw;
		private float m_cameraTargetPitch;

		private bool m_canLook = true;
		private PlayerInput m_playerInput;

		private void Awake()
		{
			m_playerInput = new PlayerInput(m_inputActionAsset.FindActionMap("Player"));

			m_charMoveController = m_character.GetComponent<CharMoveComponent>();
		}

		public void SetActiveChar(bool active)
		{
			m_character.gameObject.SetActive(active);
		}

		private void OnEnable()
		{
			m_playerInput.Enable();
			m_canLook = true;
		}
		
		private void OnDisable()
		{
			m_playerInput.Disable();
		}

		private void Update()
		{
			if (m_character == null)
			{
				enabled = false;
				return;
			}

			Move(m_playerInput.move, m_playerInput.sprint);

			if (m_playerInput.fireStarted)
			{
				m_character.attackManager.StartUse();
			}

			if (m_playerInput.fireCanceled)
			{
				m_character.attackManager.EndUse();
			}

			if (m_playerInput.reload)
			{
				m_character.attackManager.Reload();
			}
			
			if (m_playerInput.switchWeapon)
			{
				m_character.attackManager.Next();
			}
			
			if (m_playerInput.jump)
			{
				m_charMoveController.Jump();
			}
			
			if (m_playerInput.use)
			{
				onUseItem?.Invoke();
			}
		}

		private void LateUpdate()
		{
			// Обработка мыши и UI
			if (EventSystem.current.currentInputModule.input.GetMouseButtonDown(0))
			{
				m_canLook = !EventSystem.current.IsPointerOverGameObject();
			}
			else if (EventSystem.current.currentInputModule.input.GetMouseButtonUp(0))
			{
				m_canLook = true;
			}

			var look = m_canLook ? m_playerInput.look : Vector2.zero;
			CameraRotation(look);
		}

		private void Move(Vector2 move, bool isSprint)
		{
			m_charMoveController.Move(move, isSprint, m_cameraTransform.eulerAngles.y);
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

			m_charMoveController.Look(Quaternion.Euler(m_cameraTargetPitch, m_cameraTargetYaw, 0f));
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