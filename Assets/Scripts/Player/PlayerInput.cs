using UnityEngine;
using UnityEngine.InputSystem;

namespace ShadowChimera
{
	public class PlayerInput
	{
		private InputActionMap m_map;
		private InputAction m_moveAction;
		private InputAction m_lookAction;
		private InputAction m_fireAction;
		private InputAction m_reloadAction;
		private InputAction m_switchWeaponAction;
		private InputAction m_jumpAction;
		private InputAction m_sprintAction;
		private InputAction m_useAction;

		public Vector2 look => m_lookAction.ReadValue<Vector2>();
		public Vector2 move => m_moveAction.ReadValue<Vector2>();
		public bool sprint => m_sprintAction.IsPressed();
		public bool fire => m_fireAction.IsPressed();
		public bool fireStarted => m_fireAction.WasPressedThisFrame();
		public bool fireCanceled => m_fireAction.WasReleasedThisFrame();
		public bool switchWeapon => m_switchWeaponAction.WasPressedThisFrame();
		public bool reload => m_reloadAction.WasPressedThisFrame();
		public bool jump => m_jumpAction.WasPressedThisFrame();
		public bool use => m_useAction.WasPressedThisFrame();
		

		public PlayerInput(InputActionMap map)
		{
			m_map = map;
			m_moveAction = m_map.FindAction("Move", true);
			m_lookAction = m_map.FindAction("Look", true);
			m_fireAction = m_map.FindAction("Fire", true);
			m_reloadAction = m_map.FindAction("Reload", true);
			m_switchWeaponAction = m_map.FindAction("SwitchWeapon", true);
			m_jumpAction = m_map.FindAction("Jump", true);
			m_sprintAction = m_map.FindAction("Sprint", true);
			m_useAction = m_map.FindAction("Use", true);
		}

		public void Enable()
		{
			m_map.Enable();
		}

		public void Disable()
		{
			m_map.Disable();
		}
	}
}