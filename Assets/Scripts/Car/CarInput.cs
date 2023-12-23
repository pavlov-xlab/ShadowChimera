using UnityEngine.InputSystem;

namespace ShadowChimera
{
	public class CarInput
	{
		public float acces => m_accesAction.ReadValue<float>();
		public float brake => m_brakeAction.ReadValue<float>();
		public float steering => m_steeringAction.ReadValue<float>();
		public bool exitPerformed => m_exitAction.WasPerformedThisFrame();

		private readonly InputAction m_steeringAction;
		private readonly InputAction m_accesAction;
		private readonly InputAction m_brakeAction;
		private readonly InputAction m_exitAction;
		private readonly InputActionMap m_map;

		public CarInput(InputActionMap map)
		{
			m_map = map;

			m_steeringAction = map.FindAction("Steering", true);
			m_accesAction = map.FindAction("Acces", true);
			m_brakeAction = map.FindAction("Brake", true);
			m_exitAction = map.FindAction("Exit", true);
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