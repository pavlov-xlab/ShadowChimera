using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShadowChimera
{
    public class CarInputController : MonoBehaviour
    {
        [SerializeField] private CarPhysic m_car;
		[SerializeField] private CinemachineVirtualCamera m_camera;
		[SerializeField] private InputActionAsset m_inputActionAsset;

		public event System.Action onExitCar;

		private CarInput m_carInput;

		private void Awake()
		{
			m_carInput = new CarInput(m_inputActionAsset.FindActionMap("Car"));
		}

		private void OnEnable()
		{
			m_carInput.Enable();
		}

		private void OnDisable()
		{
			m_carInput.Disable();
		}

		public void SetCar(CarPhysic car)
        {
	        if (m_car)
	        {
		        m_car.ResetInput();
	        }
	        
            m_car = car;
            
            m_camera.Follow = car ? car.transform : null;
			m_camera.LookAt = car ? car.transform : null;
		}

		private void Start()
		{
			if (m_car != null)
			{
				SetCar(m_car);
			}
		}

		private void Update()
		{
            if (m_car)
            {
				m_car.SetInput(m_carInput.acces, m_carInput.brake, m_carInput.steering, 0f);

				if (m_carInput.exitPerformed)
				{
					onExitCar?.Invoke();
				}
            }
		}
	}
}
