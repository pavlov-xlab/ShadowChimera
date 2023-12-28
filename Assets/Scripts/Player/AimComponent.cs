using System;
using UnityEngine;

namespace ShadowChimera
{
    public class AimComponent : MonoBehaviour
    {
        private Transform m_cameraTr;
        [SerializeField] private float m_maxDistance = 25f;
        [SerializeField] LayerMask m_layerMask;
        
        public Vector3 aimTargetPoint { private set; get; }

        private void Start()
        {
            m_cameraTr = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            Ray ray = new Ray()
            {
                direction = m_cameraTr.forward,
                origin = m_cameraTr.position,
            };
            
            var result = Physics.Raycast(ray, out var hitInfo, m_maxDistance, m_layerMask, QueryTriggerInteraction.Ignore);
            if (result)
            {
                aimTargetPoint = hitInfo.point;
            }
            else
            {
                aimTargetPoint = ray.origin + ray.direction * m_maxDistance;
            }
        }
    }
}
