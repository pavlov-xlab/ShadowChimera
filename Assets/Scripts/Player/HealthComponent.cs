using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class HealthComponent : MonoBehaviour, IDamageable
	{
		[SerializeField] private float m_healthMax = 100f;
        [SerializeField] private float m_health = 100f;

        public event System.Action<float> onTakeDamage;

		public bool isFullHealth => m_health == m_healthMax;
        public float healthPercent => m_health / m_healthMax;

        public void TakeDamage(float damage)
        {
            damage = Mathf.Min(damage, m_health);

            m_health -= damage;
			
            onTakeDamage?.Invoke(damage);

			if (m_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
