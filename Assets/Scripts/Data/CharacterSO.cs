using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	[CreateAssetMenu(fileName = "CharacterSO", menuName = "ShadowChimera/CharacterSO")]
	public class CharacterSO : ScriptableObject
    {
        public MoveData moveData;
		public HealthData healthData;
		public List<WeaponSO> weapons;
    }

	[System.Serializable]
	public class HealthData
	{
		public float health = 100;
		public float maxHealth = 100;
	}


	[System.Serializable]
	public class MoveData
	{
		public float speed = 5f;
		public float sprintSpeed = 10f;
	}
}
