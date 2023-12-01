using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShadowChimera
{
    public class PauseState : GameState
    {
		public GameplayState gameplayState;

		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
		}

		public void Restart()
		{
			var scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		}

		public void Reume()
		{
			Exit();
			gameplayState.Enter();
		}
	}
}
