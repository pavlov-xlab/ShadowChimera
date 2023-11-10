using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace ShadowChimera
{
	public class FindTargetAction : ActionNode
	{
		private SearcherTarget m_searcherTarget;
		public float maxDistance = 10;

		protected override void OnStart()
		{
			if (m_searcherTarget == null)
			{
				m_searcherTarget = context.gameObject.GetComponent<SearcherTarget>();
			}
		}

		protected override void OnStop()
		{	
		}

		protected override State OnUpdate()
		{
			if (blackboard.target)
			{
				if (Vector3.Distance(blackboard.target.position, context.transform.position) < maxDistance)
				{
					return State.Success;
				}
			}

			blackboard.target = m_searcherTarget.FindTarget();

			if (blackboard.target)
			{
				blackboard.moveToPosition = blackboard.target.position;
				return State.Success;
			}

			return State.Failure;
		}
	}
}
