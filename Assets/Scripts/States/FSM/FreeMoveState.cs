using UnityEngine;

namespace ShadowChimera
{
	public class FreeMoveState : BaseState
	{
		public FreeMoveState(GameplayContext context) : base(context)
		{
			SetPause(true);
		}


		public override void SetPause(bool pause)
		{
			if (context.playerInputUI)
			{
				context.playerInputUI.SetActive(!pause);
			}

			if (context.playerController)
			{
				context.playerController.enabled = !pause;
			}

			if (!pause)
			{
				context.cameraManager.Activate(CameraNames.Player);
			}
		}

		public override void Enter()
		{
			var playerController = context.playerController;

			playerController.SetCharacter(context.character);
			playerController.onUseItem += OnUseItem;

			SetPause(false);
		}

		public override void Exit()
		{
			SetPause(true);

			var playerController = context.playerController;
			if (playerController)
			{
				playerController.onUseItem -= OnUseItem;
			}
		}

		private void OnUseItem(GameObject item)
		{
			var car = item.GetComponentInParent<CarPhysic>();
			if (car)
			{
				context.car = car;
			}
		}
	}
}