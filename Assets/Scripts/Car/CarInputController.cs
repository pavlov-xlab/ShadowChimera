using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShadowChimera
{
    public class CarInputController : MonoBehaviour
    {
        [SerializeField] private CarPhysicController m_car;
		[SerializeField] private CinemachineVirtualCamera m_camera;
		[SerializeField] private InputActionAsset m_inputActionAsset;

		public event System.Action onExitCar;

		private CarControl m_carControl;

		private void Awake()
		{
			m_carControl = new CarControl(m_inputActionAsset.FindActionMap("Car"));
		}

		private void OnEnable()
		{
			m_carControl.Enable();
		}

		private void OnDisable()
		{
			m_carControl.Disable();
		}

		public void Init(CarPhysicController car)
        {
            m_car = car;
            
            m_camera.Follow = car.transform;
			m_camera.LookAt = car.transform;
		}

		private void Start()
		{
			if (m_car != null)
			{
				Init(m_car);
			}
		}

		private void Update()
		{
            if (m_car)
            {
				m_car.SetInput(m_carControl.acces, m_carControl.brake, m_carControl.steering);

				if (m_carControl.exitPerformed)
				{
					onExitCar?.Invoke();
				}
            }
		}
	}


	public class CarControl
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

		public CarControl(InputActionMap map)
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
