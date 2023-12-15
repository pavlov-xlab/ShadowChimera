using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ShadowChimera
{
    public class CharAnimsComponent : MonoBehaviour
    {
		[SerializeField] private Character m_character;
		[SerializeField] private Animator m_animator;

		private void Awake()
		{
			if (m_animator == null)
			{
				m_animator = GetComponent<Animator>();
			}

			if (m_character == null)
			{
				m_character = GetComponent<Character>();
			}

			m_character = GetComponentInParent<Character>();

			m_character.healtCompont.onDie += () =>
			{
				m_animator.SetTrigger("Die");
			};
		}

		private void Update()
		{
			var speed = m_character.moveComponent.velocity.magnitude;
			m_animator.SetFloat("Speed", speed);
		}

		
	}
}
