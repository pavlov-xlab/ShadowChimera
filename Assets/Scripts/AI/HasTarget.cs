using TheKiwiCoder;
using UnityEngine;

namespace ShadowChimera
{
	public class HasTarget : DecoratorNode
	{
		public float maxDistance = 10f;
		
		protected override void OnStart()
		{
			
		}

		protected override void OnStop()
		{
			
		}

		protected override State OnUpdate()
		{
			if (blackboard.target == null || Vector3.Distance(blackboard.target.position, context.transform.position) > maxDistance)
			{
				if (child is { state: State.Running })
				{
					child.Abort();
				}
				
				return State.Failure;
			}
			
			return child?.Update() ?? State.Running;
		}
	}
}