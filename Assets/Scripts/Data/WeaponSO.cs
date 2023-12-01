using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "ShadowChimera/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
		[SerializeField] private WeaponAttackItem m_prefab;
		[SerializeField] private float m_delay = 0.1f;
		[SerializeField] private float m_reloadTime = 1f;
		[SerializeField] private int m_bulletCount = 90;
		[SerializeField] private int m_maxCage = 30;
		[SerializeField] private int m_cage = 0;
		[SerializeField] private bool m_autoFire = true;
		[SerializeField] private bool m_autoReload = true;

		public WeaponAttackItem prefab => m_prefab;
		public float delay => m_delay;
		public float reloadTime => m_reloadTime;
		public int bulletCount => m_bulletCount;
		public int maxCage => m_maxCage;
		public int cage => m_cage;
		public bool autoFire => m_autoFire;
		public bool autoReload => m_autoReload;
	}
}
