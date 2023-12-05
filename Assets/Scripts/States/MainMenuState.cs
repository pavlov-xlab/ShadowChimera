using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShadowChimera
{
    public class MainMenuState : GameState
    {
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void GotoSettings()
        {
            States.instance.Push<SettingsState>();
        }
    }
}
