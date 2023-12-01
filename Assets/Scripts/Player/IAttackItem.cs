using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
	public interface IAttackItem
	{
		void StartUse();
		void EndUse();
		void Reload();

		void Show();
		void Hide();

		void DestroySelf();
	}
}
