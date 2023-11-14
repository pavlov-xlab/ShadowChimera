using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace ShadowChimera
{
	public class FindTarget : ActionNode
	{
		private SearcherTarget m_searcherTarget;

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
			blackboard.target = m_searcherTarget.FindTarget();
			if (blackboard.target)
			{
				blackboard.moveToPosition = blackboard.target.position;
				return State.Success;
			}

			return blackboard.target ? State.Success : State.Failure;
		}
	}
}
