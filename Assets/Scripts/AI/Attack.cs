using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShadowChimera
{
	public class Attack : ActionNode
	{
		public float delay = 1f;

		AttackManager manager;
		float m_timer;

		protected override void OnStart()
		{
			if (manager == null)
			{
				manager = context.gameObject.GetComponent<AttackManager>();
			}

			m_timer = Time.time;

			if (manager != null)
			{
				manager.StartUse();
			}
		}

		protected override void OnStop()
		{
			if (manager != null)
			{
				manager.EndUse();
			}
		}

		protected override State OnUpdate()
		{
			return (Time.time - m_timer) < delay ? State.Running : State.Success;
		}
	}
}
