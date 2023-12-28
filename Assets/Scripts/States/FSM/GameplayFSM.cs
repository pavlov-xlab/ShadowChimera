using System.Collections.Generic;

namespace ShadowChimera
{
	public class GameplayFSM
	{
		private readonly GameplayContext m_context;
		private readonly List<Transition> m_transitions = new();
		private BaseState m_currentState;

		public GameplayFSM(GameplayContext context)
		{
			m_context = context;

			var freeMoveState = new FreeMoveState(context);
			var carRaceState = new CarRaceState(context);

			m_transitions.Add(new Transition(freeMoveState, carRaceState, () => m_context.car != null));
			m_transitions.Add(new Transition(carRaceState, freeMoveState, () => m_context.inCar == false));

			m_currentState = freeMoveState;
			m_currentState.Enter();
		}

		public void Activate()
		{
			m_currentState.SetPause(false);
		}
		
		public void Deactivate()
		{
			m_currentState.SetPause(true);
		}

		public void Update()
		{
			foreach (var transition in m_transitions)
			{
				if (transition.prevState == m_currentState)
				{
					if (transition.Check())
					{
						ProcessTransition(transition);
						break;
					}
				}
			}
		}

		private void ProcessTransition(Transition tr)
		{
			tr.prevState.Exit();
			tr.nextState.Enter();
			m_currentState = tr.nextState; 
		}
	}
}