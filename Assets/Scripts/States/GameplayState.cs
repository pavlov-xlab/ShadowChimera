using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShadowChimera
{
    public class GameplayState : GameState
	{
		public PauseState pauseState;
		public PlayerController playerController;

		protected override void OnEnable()
		{
			base.OnEnable();
			playerController.enabled = true;
		}

		public void GotoPause()
		{
			Exit();
			pauseState.Enter();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			playerController.enabled = false;
		}
	}
}
