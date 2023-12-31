using System;
using UnityEngine;

namespace ShadowChimera
{
    public class CharAnimsComponent : MonoBehaviour
    {
		[SerializeField] private Character m_character;
		[SerializeField] private Animator m_animator;

		private static int SpeedId = Animator.StringToHash("Speed");
		private static int DieId = Animator.StringToHash("Die");
		private static int ShootId = Animator.StringToHash("Shoot");
		private static int JumpId = Animator.StringToHash("Jump");
		private static int GroundedId = Animator.StringToHash("Grounded");
		private static int FreeFallId = Animator.StringToHash("FreeFall");
		private static int DrivingId = Animator.StringToHash("Driving");

		private IMoveComponent moveComponent => m_character.moveComponent;
		
		private float m_lastTimeGrounded = 0f;

		private void Start()
		{
			if (m_animator == null)
			{
				m_animator = GetComponent<Animator>();
			}

			if (m_character == null)
			{
				m_character = GetComponentInParent<Character>();
			}

			m_character.healthComponent.onDie += () =>
			{
				m_animator.SetTrigger(DieId);
			};

			m_character.attackManager.onUse += () =>
			{
				m_animator.SetTrigger(ShootId);
			};

			moveComponent.onJump += () =>
			{
				m_animator.SetBool(GroundedId, false);
				m_animator.SetBool(JumpId, true);
			};
		}

		private void LateUpdate()
		{
			var speed = moveComponent.velocity.magnitude;
			m_animator.SetFloat(SpeedId, speed);

			if (moveComponent.isGrounded)
			{
				m_animator.SetBool(GroundedId, true);
				m_animator.SetBool(FreeFallId, false);
				m_animator.SetBool(JumpId, false);

				m_lastTimeGrounded = Time.time;
			}
			else
			{
				m_animator.SetBool(GroundedId, false);

				if (Time.time - m_lastTimeGrounded > 0.1f)
				{
					m_animator.SetBool(FreeFallId, true);
				}
			}

			bool driving = m_character.transform.parent != null;
			m_animator.SetBool(DrivingId, driving);
		}

		private void OnAnimatorIK(int layerIndex)
		{
			if (m_character.aimComponent)
			{
				m_animator.SetLookAtPosition(m_character.aimComponent.aimTargetPoint);
				m_animator.SetLookAtWeight(1f, 0.3f, 0.75f);
			}
		}
    }
}
