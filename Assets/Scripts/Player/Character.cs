using System;
using UnityEngine;

namespace ShadowChimera
{
    public class Character : MonoBehaviour
    {
		[SerializeField] private AttackManager m_attackManager;
		[SerializeField] private HealthComponent m_healthComponent;
		[SerializeField] private AimComponent m_aimComponent;
		private IMoveComponent m_moveComponent;

		public AttackManager attackManager => m_attackManager;
		public HealthComponent healthComponent => m_healthComponent;
		public IMoveComponent moveComponent => m_moveComponent;
		public AimComponent aimComponent => m_aimComponent;

		public void Initialize(CharacterSO data)
		{
			if (attackManager)
			{
				attackManager.Initialize(data.weapons);
			}

			if (m_healthComponent)
			{
				m_healthComponent.Initialize(data.healthData.maxHealth, data.healthData.health);
			}

			if (m_moveComponent != null)
			{
				m_moveComponent.Init(data.moveData.speed, data.moveData.sprintSpeed);
			}
		}

		private void Awake()
		{
			if (m_attackManager == null)
			{
				m_attackManager = GetComponent<AttackManager>();
			}

			if (m_healthComponent == null)
			{
				m_healthComponent = GetComponent<HealthComponent>();
			}
			
			if (m_aimComponent == null)
			{
				m_aimComponent = GetComponent<AimComponent>();
			}

			m_moveComponent = GetComponent<IMoveComponent>();
			
			
		}

		private void OnEnable()
		{
			m_moveComponent.enabled = true;
		}

		private void OnDisable()
		{
			m_moveComponent.enabled = false;
		}
	}
}
