using UnityEngine;

namespace ShadowChimera
{
	public class MBLogger : MonoBehaviour
	{
		private void Print(string text)
		{
			Debug.Log($"[{name}:{Time.frameCount}] {text}");
		}

		private void Awake()
		{
			Print("Awake");
		}

		private void Start()
		{
			Print("Start");
		}

		private void OnEnable()
		{
			Print("OnEnable");
		}

		private void OnDisable()
		{
			Print("OnDisable");
		}

		private void OnDestroy()
		{
			Print("OnDestroy");
		}
	}
}