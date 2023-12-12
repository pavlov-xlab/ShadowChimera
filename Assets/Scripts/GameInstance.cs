using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace ShadowChimera
{
	public class GameInstance : MonoBehaviour
    {
        public static GameInstance instance;
		
		public Player player { private set; get; } = new Player();
		[SerializeField] private AudioMixer m_audioMixer;

		private void Awake()
		{
			if (instance != null)
			{
				Destroy(gameObject);
				return;
			}

			instance = this;
			DontDestroyOnLoad(gameObject);


			player.Load();
			ApplySettings();
		}

		public void ApplySettings()
		{
			QualitySettings.SetQualityLevel(player.settings.quality);
			m_audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80f, 0f, player.settings.musicVolume / 100f));
			m_audioMixer.SetFloat("SfxVolume", Mathf.Lerp(-80f, 0f, player.settings.fxVolume / 100f));
		}

		private void OnApplicationQuit()
		{
			player.Save();
		}


		[RuntimeInitializeOnLoadMethod]
		static void OnRuntimeInitialized()
		{
			var instance = FindAnyObjectByType<GameInstance>();
			if (instance == null)
			{
				var prefab = Resources.Load<GameInstance>("GameInstance");
				var go = Instantiate(prefab);
				go.name = go.name.Replace("(Clone)", string.Empty);
			}
		}
	}
}
