namespace ShadowChimera
{
	public abstract class BaseState
	{
		protected readonly GameplayContext context;

		protected BaseState(GameplayContext context)
		{
			this.context = context;
		}
		
		public abstract void SetPause(bool pause);
		public abstract void Enter();
		public abstract void Exit();
	}
}