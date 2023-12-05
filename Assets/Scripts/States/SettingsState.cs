using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class SettingsState : GameState
    {
        public void Back()
        {
            States.instance.Pop();
        }
    }
}
