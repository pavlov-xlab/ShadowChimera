using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ShadowChimera
{
    public class HealthComponent : MonoBehaviour, IDamageable
	{
		[SerializeField] private float m_healthMax = 100f;
        [SerializeField] private float m_health = 100f;
        [SerializeField] private UnityEvent m_onDie;

        public bool isDie => m_health <= 0f;

        public event System.Action<float> onTakeDamage;
		public event System.Action onDie;


		public bool isFullHealth => m_health == m_healthMax;
        public float healthPercent => m_health / m_healthMax;

        public void Initialize(float max, float initHp)
        {
			m_healthMax = max;
            m_health = initHp;
            onTakeDamage?.Invoke(0);
		}

        public void TakeDamage(float damage)
        {
            damage = Mathf.Min(damage, m_health);

            m_health -= damage;
			
            onTakeDamage?.Invoke(damage);

			if (m_health <= 0)
            {
				onDie?.Invoke();
				m_onDie.Invoke();
				//Destroy(gameObject);
			}
        }
    }
}
