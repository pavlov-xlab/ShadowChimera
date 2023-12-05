using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShadowChimera
{
    public class GameplayState : GameState
	{
		public void GotoPause()
		{
			States.instance.Push<PauseState>();
		}
	}
}
