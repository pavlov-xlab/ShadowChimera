using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShadowChimera
{
	public class NavMeshAgentMoveComponent : MonoBehaviour, IMoveComponent
	{
		[SerializeField] private NavMeshAgent m_agent;

		public Vector3 velocity => m_agent.velocity;
		public bool isGrounded => true;
		public event Action onJump;

		public void Init(float speed, float sprintSpeed)
		{
			m_agent.speed = speed;
		}
	}
}
