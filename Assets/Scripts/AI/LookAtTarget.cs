using TheKiwiCoder;
using UnityEngine;

namespace ShadowChimera.AI
{
	public class LookAtTarget : ActionNode
	{
		protected override void OnStart()
		{
		}

		protected override void OnStop()
		{
		}

		protected override State OnUpdate()
		{
			var targetPosition = blackboard.target.position;
			context.transform.LookAt(targetPosition, Vector3.up);
			return State.Running;
		}
	}
}