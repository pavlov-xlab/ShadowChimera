using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace ShadowChimera
{
	public class DestroySelf : ActionNode
	{
		protected override void OnStart()
		{
			
		}

		protected override void OnStop()
		{
			
		}

		protected override State OnUpdate()
		{
			Object.Destroy(context.gameObject);
			return State.Success;
		}
	}
}
