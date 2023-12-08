using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShadowChimera
{
    public class GameplayState : GameState
	{
		[SerializeField] CameraManager m_cameraManager;

		private void OnEnable()
		{
			base.OnEnable();

			m_cameraManager.Activate(CameraNames.Player);
		}

		public void GotoPause()
		{
			States.instance.Push<PauseState>();
		}
	}
}
