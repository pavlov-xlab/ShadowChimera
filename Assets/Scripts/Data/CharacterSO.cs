using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	[CreateAssetMenu(fileName = "CharacterSO", menuName = "ShadowChimera/CharacterSO")]
	public class CharacterSO : ScriptableObject
    {
        public float speedMove;
		public HealthData healthData;
		public List<WeaponSO> weapons;
    }

	[System.Serializable]
	public class HealthData
	{
		public float health;
		public float maxHealth;
	}
}
