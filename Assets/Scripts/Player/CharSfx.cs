using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class CharSfx : MonoBehaviour
    {
		[SerializeField] private AudioSource m_audioSource;
		[SerializeField] private List<AudioClip> m_stepAudioClips;
		[SerializeField] private AudioClip m_landAudioClips;

		public void OnFootstep()
		{
			if (m_audioSource != null)
			{
				var index = Random.Range(0, m_stepAudioClips.Count);
				m_audioSource.PlayOneShot(m_stepAudioClips[index]);
			}
		}

		public void OnLand()
		{
			m_audioSource.PlayOneShot(m_landAudioClips);
		}
	}
}
