using UnityEngine;

namespace ShadowChimera
{
    public class ItemDetector : MonoBehaviour
    {
        private GameObject m_lastItem;

        public GameObject lastItem => m_lastItem;
        
        private void OnTriggerEnter(Collider other)
        {
            m_lastItem = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == m_lastItem)
            {
                m_lastItem = null;
            }
        }
    }
}
