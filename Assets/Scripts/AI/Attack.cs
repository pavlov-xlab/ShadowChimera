using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace ShadowChimera
{
	public class Attack : ActionNode
	{
		AttackManager manager;

		protected override void OnStart()
		{
			if (manager == null)
			{
				manager = context.gameObject.GetComponent<AttackManager>();
			}
		}

		protected override void OnStop()
		{
			
		}

		protected override State OnUpdate()
		{
			if (manager != null)
			{
				manager.StartUse();
				//manager.EndUse();
			}
			return State.Success;
		}
	}
}
