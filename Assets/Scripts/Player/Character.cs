using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class Character : MonoBehaviour
    {
		[SerializeField] private AttackManager m_attackManager;
		[SerializeField] private HealthComponent m_healtCompont;
		private IMoveComponent m_moveComponent;

		public AttackManager attackManager => m_attackManager;
		public HealthComponent healtCompont => m_healtCompont;
		public IMoveComponent moveComponent => m_moveComponent;

		public void Initialize(CharacterSO data)
		{
			if (attackManager)
			{
				attackManager.Initialize(data.weapons);
			}

			if (m_healtCompont)
			{
				m_healtCompont.Initialize(data.healthData.maxHealth, data.healthData.health);
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

			if (m_healtCompont == null)
			{
				m_healtCompont = GetComponent<HealthComponent>();
			}

			m_moveComponent = GetComponent<IMoveComponent>();
		}
	}
}
