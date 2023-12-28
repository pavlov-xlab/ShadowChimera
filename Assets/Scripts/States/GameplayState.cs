using System;
using UnityEngine;

namespace ShadowChimera
{
    public class GameplayState : GameState
	{
		[SerializeField] CameraManager m_cameraManager;

		[SerializeField] PlayerController m_playerController;
		[SerializeField] CarInputController m_carController;
		[SerializeField] Character m_character;
		
		[SerializeField] GameObject m_playerInputUI;
		[SerializeField] GameObject m_carInputUI;

		private GameplayFSM m_gameplayFsm;

		private void Awake()
		{
			var context = new GameplayContext()
			{
				carController = m_carController,
				playerController = m_playerController,
				character = m_character,
				cameraManager =  m_cameraManager,
				carInputUI = m_carInputUI,
				playerInputUI = m_playerInputUI
			};
			m_gameplayFsm = new GameplayFSM(context);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			
			m_gameplayFsm.Activate();
		}
		

		protected override void OnDisable()
		{
			base.OnDisable();

			m_gameplayFsm.Deactivate();
		}

		private void Update()
		{
			m_gameplayFsm.Update();
		}

		public void GotoPause()
		{
			States.instance.Push<PauseState>();
		}
	}
}
