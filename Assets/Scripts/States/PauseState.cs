using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShadowChimera
{
    public class PauseState : GameState
    {
		[SerializeField] CameraManager m_cameraManager;

		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;

			m_cameraManager.Activate(CameraNames.Pause);
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
			States.instance.Pop();
		}

		public void GotoMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
