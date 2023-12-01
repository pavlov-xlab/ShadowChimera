using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class InitCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSO m_data;
		[SerializeField] private Character m_caharacter;

		private void Start()
        {
            if (m_caharacter != null)
            {
                m_caharacter.Initialize(m_data);
            }
		}
    }
}
