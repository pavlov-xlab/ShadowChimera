using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	public class ShootBullet : MonoBehaviour, IShoot
	{
		[SerializeField] private BulletComponent m_prefab;
		[SerializeField] private Transform m_muzzle;

		public void Shoot()
		{
			Instantiate(m_prefab, m_muzzle.position, m_muzzle.rotation);
		}
	}
}
