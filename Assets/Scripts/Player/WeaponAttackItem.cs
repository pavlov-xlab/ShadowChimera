using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	public enum WeaponState
	{
		Idle, Fire, Reload,
	}

	public class WeaponAttackItem : MonoBehaviour, IAttackItem
	{
		[SerializeField] private float m_delay = 0.1f;
		[SerializeField] private float m_reloadTime = 1f;
		[SerializeField] private int m_bulletCount = 90;
		[SerializeField] private int m_maxCage = 30;
		[SerializeField] private int m_cage = 0;
		[SerializeField] private bool m_autoFire = true;
		[SerializeField] private bool m_autoReload = true;

		public bool canFire => m_cage > 0;

		private WeaponState m_state = WeaponState.Idle;
		private float m_timer = 0f;

		private IShoot m_shoot;

		private void Awake()
		{
			m_shoot = GetComponent<IShoot>();
		}

		private void Start()
		{
			InternalReload();
		}

		public void EndUse()
		{
			Debug.Log("End Use", this);

			m_state = WeaponState.Idle;
		}

		public void StartUse()
		{
			if (m_state == WeaponState.Fire)
			{
				return;
			}

			m_state = WeaponState.Fire;
			m_timer = m_delay;

			Debug.Log("Start Use", this);
		}

		private void Update()
		{
			m_timer += Time.deltaTime;

			switch (m_state)
			{
				case WeaponState.Idle:
					break;

				case WeaponState.Fire:
					if (m_timer >= m_delay)
					{
						m_timer = 0;

						if (canFire)
						{
							m_shoot.Shoot();
							m_cage--;
						}

						if (!m_autoFire)
						{
							m_state = WeaponState.Idle;
						}
						else if (m_autoReload && !canFire)
						{
							Reload();
						}
					}
					break;

				case WeaponState.Reload:
					if (m_timer >= m_reloadTime)
					{
						InternalReload();

						if (m_autoFire && canFire)
						{
							StartUse();
						}
					}
					break;
			}
		}

		public void Reload()
		{
			if (m_state == WeaponState.Reload)
			{
				return;
			}

			m_timer = 0;
			m_state = WeaponState.Reload;
		}

		private void InternalReload()
		{
			Debug.Log("Reload");
			m_bulletCount = Mathf.Max(m_bulletCount - m_maxCage, 0);
			m_cage = Mathf.Min(m_maxCage, m_bulletCount);

			m_state = WeaponState.Idle;
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
