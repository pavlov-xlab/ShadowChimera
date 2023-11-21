using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace ShadowChimera
{
    public class AutoBuildNavMesh : MonoBehaviour
    {
		private void Awake()
		{
			var surfaces = GetComponents<NavMeshSurface>();
			foreach (var surface in surfaces)
			{
				surface.BuildNavMesh();
			}
		}
	}
}
