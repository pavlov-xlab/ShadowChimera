using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class CharAnimsComponent : MonoBehaviour
    {
		private IMoveComponent m_moveComponent;
		[SerializeField] private Animator m_animator;

		private void Awake()
		{
			if (m_animator == null)
			{
				m_animator = GetComponent<Animator>();
			}

			Character character = GetComponentInParent<Character>();
			m_moveComponent = character.moveComponent;
			character.healtCompont.onDie += () =>
			{
				m_animator.SetTrigger("Die");
			};
		}

		private void Update()
		{
			if (m_moveComponent != null)
			{
				var speed = m_moveComponent.velocity.magnitude;
				m_animator.SetFloat("Speed", speed);
			}
		}

		
	}
}
