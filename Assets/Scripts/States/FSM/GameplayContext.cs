using UnityEngine;

namespace ShadowChimera
{
	public class GameplayContext
	{
		public Character character;
		public CarPhysic car;
		public PlayerController playerController;
		public CarInputController carController;
		public bool inCar = false;
		public CameraManager cameraManager;
		public GameObject playerInputUI;
		public GameObject carInputUI;
	}
}