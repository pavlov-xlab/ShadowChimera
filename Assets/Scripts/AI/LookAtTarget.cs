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
			// Сделать плавный поворот к целе
			var targetPosition = blackboard.target.position;
			targetPosition.y = context.transform.position.y;
			context.transform.LookAt(targetPosition, Vector3.up);
			return State.Running;
		}
	}
}