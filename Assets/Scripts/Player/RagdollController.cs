using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public class RagdollController : MonoBehaviour
    {
		private void Awake()
		{
			SetActivate(false);
		}

		public void SetActivate(bool value)
		{
			var bodys = GetComponentsInChildren<Rigidbody>();
			foreach (var body in bodys)
			{
				body.isKinematic = !value;
			}

			var colliders = GetComponentsInChildren<Collider>();
			foreach (var collider in colliders)
			{
				collider.enabled = value;
			}
		}
	}
}
