using UnityEngine;

namespace ShadowChimera
{
	public class CarRaceState : BaseState
	{
		public CarRaceState(GameplayContext context) : base(context)
		{
			SetPause(true);
		}

		public override void SetPause(bool pause)
		{
			if (context.carController)
			{
				context.carController.enabled = !pause;
			}

			if (context.carInputUI)
			{
				context.carInputUI.SetActive(!pause);
			}
			
			if (!pause)
			{
				context.cameraManager.Activate(CameraNames.Car);
			}
		}

		public override void Enter()
		{
			context.inCar = true;
			
			var carController = context.carController;
			var character = context.character;

			
			var characterTr = character.transform;
			character.enabled = false;
			characterTr.SetParent(context.car.driverPoint);
			characterTr.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			
			carController.SetCar(context.car);
			carController.onExitCar += OnExitCar;
			
			SetPause(false);
		}

		public override void Exit()
		{
			SetPause(true);

			var carController = context.carController;
			if (carController == null)
			{
				return;
			}
			
			var character = context.character;
			if (character == null)
			{
				return;
			}


			var characterTr = character.transform;
			var exitPoint = context.car.exitPoint;

			characterTr.SetParent(null);
			characterTr.SetPositionAndRotation(exitPoint.position, exitPoint.rotation);
			character.enabled = true;

			carController.SetCar(null);
			carController.onExitCar -= OnExitCar;

			context.car = null;
		}

		private void OnExitCar()
		{
			context.inCar = false;
		}
	}
}