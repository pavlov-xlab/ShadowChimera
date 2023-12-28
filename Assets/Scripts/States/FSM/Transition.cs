using System.Collections.Generic;

namespace ShadowChimera
{
	public class Transition
	{
		public readonly BaseState prevState;
		public readonly BaseState nextState;
		private readonly List<System.Func<bool>> m_conditions = new();

		public Transition(BaseState prevState, BaseState nextState, params System.Func<bool>[] condition)
		{
			this.prevState = prevState;
			this.nextState = nextState;
			m_conditions.AddRange(condition);
		}

		public bool Check()
		{
			if (m_conditions.Count == 0)
			{
				return false;
			}
			
			foreach (var condition in m_conditions)
			{
				if (!condition.Invoke())
				{
					return false;
				}
			}
			return true;
		}
	}
}