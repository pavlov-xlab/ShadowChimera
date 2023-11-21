using System;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace ShadowChimera
{
	public class NextPoints : ActionNode
	{
		private List<Vector3> m_points = new();
		private int m_index = 0;

		protected override void OnStart()
		{
			if (m_points.Count == 0)
			{
				var pointsContainer = context.gameObject.GetComponent<PointsContainer>();
				if (pointsContainer == null)
				{
					Debug.LogError("PointsContainer not found!");
					return;
				}

				m_points = pointsContainer.GetPoints();
			}
		}

		protected override void OnStop()
		{
			
		}

		protected override State OnUpdate()
		{
			if (m_points.Count == 0)
			{
				return State.Failure;
			}

			if (m_index >= m_points.Count)
			{
				m_index = 0;
			} 

			blackboard.moveToPosition = m_points[m_index++];

			return State.Success;
		}
	}
}
