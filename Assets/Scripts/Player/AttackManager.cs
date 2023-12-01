using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class AttackManager : MonoBehaviour
    {
		private List<IAttackItem> m_items = new();
		private int m_currentIndex = -1;

		public IAttackItem currentItem
		{
			get
			{
				if (m_currentIndex >= 0)
				{
					return m_items[m_currentIndex];
				}
				return null;
			}
		}


		public void Initialize(IReadOnlyList<WeaponSO> weapons)
		{
			m_currentIndex = -1;
			foreach( var item in m_items)
			{
				item.DestroySelf();
			}
			m_items.Clear();

			foreach( var weaponData in weapons)
			{
				var item = Instantiate(weaponData.prefab, transform);
				item.Initialize(weaponData);
				item.Hide();
				m_items.Add(item);
			}


			m_currentIndex = m_items.Count > 0 ? 0 : -1;
			currentItem?.Show();
		}

		private void Awake()
		{
			GetComponentsInChildren<IAttackItem>(true, m_items);

			for (int i = 1; i < m_items.Count; i++)
			{
				m_items[i].Hide();
			}

			m_currentIndex = m_items.Count > 0 ? 0 : -1;
			currentItem?.Show();
		}

		public void Next()
		{
			currentItem?.Hide();

			if (m_items.Count > 0)
			{
				m_currentIndex++;
				if (m_currentIndex >= m_items.Count)
				{
					m_currentIndex = 0;
				}

				currentItem?.Show();
			}
		}

		public void StartUse()
		{
			currentItem?.StartUse();
		}

		public void EndUse()
		{
			currentItem?.EndUse();
		}


		public void Reload()
		{
			currentItem?.Reload();
		}
	}
}
