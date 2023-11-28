using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	public class ShootRaycast : MonoBehaviour, IShoot
	{
		[SerializeField] private Transform m_muzzle;
		[SerializeField] private float m_damage = 10f;

		public void Shoot()
		{
			var ray = new Ray(m_muzzle.position, m_muzzle.forward);
			if (Physics.Raycast(ray, out var hitInfo, 100f))
			{
				var damageable = hitInfo.transform.GetComponentInParent<IDamageable>();
				if (damageable != null)
				{
					damageable.TakeDamage(m_damage);
				}
			}
		}
	}
}
