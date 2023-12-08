using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	public class GameInstance : MonoBehaviour
    {
        public static GameInstance instance;
		
		public Player player { private set; get; } = new Player();

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
