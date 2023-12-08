using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class SettingsState : GameState
    {
        [SerializeField] private UISettingsPanel m_settingsPanel;

		private Player.Settings settings => GameInstance.instance.player.settings;

		protected override void OnEnable()
		{
			base.OnEnable();			

            m_settingsPanel.SetMusic(settings.musicVolume);
			m_settingsPanel.SetFx(settings.fxVolume);
			m_settingsPanel.SetQuility(settings.quality);

			m_settingsPanel.onQualityChanged += OnQualityChanged;
			m_settingsPanel.onMusicVolumeChanged += OnMusicVolumeChanged;
			m_settingsPanel.onFxVolumeChanged += OnFxColumeChanged;
		}

		private void OnFxColumeChanged(int obj)
		{
			settings.fxVolume = obj;
		}

		private void OnMusicVolumeChanged(int obj)
		{
			settings.musicVolume = obj;
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			m_settingsPanel.onQualityChanged -= OnQualityChanged;
			m_settingsPanel.onMusicVolumeChanged -= OnMusicVolumeChanged;
			m_settingsPanel.onFxVolumeChanged -= OnFxColumeChanged;

			GameInstance.instance.ApplySettings();
		}

		private void OnQualityChanged(int index)
		{
			settings.quality = index;
		}

		public void Back()
        {
            States.instance.Pop();
        }

		
    }
}
