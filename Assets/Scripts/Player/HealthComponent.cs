using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class HealthComponent : MonoBehaviour, IDamageable
	{
        [SerializeField] private float m_health = 100f;

        public void TakeDamage(float damage)
        {
            m_health -= damage;
            if (m_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
