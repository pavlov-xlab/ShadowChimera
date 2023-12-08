using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public static class CameraNames
    {
        public const string Player = "PlayerController";
		public const string Dead = "DeadCamera";
		public const string Pause = "PauseCamera";
	}

    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCamera> m_cameras;
        [SerializeField] private string m_startCamera;

		private void Awake()
		{
            Activate(m_startCamera);
		}

		public void Activate(string cameraName)
        {
            foreach (var cam in m_cameras)
            {
                cam.enabled = cameraName == cam.name;
            }
        }
    }
}
