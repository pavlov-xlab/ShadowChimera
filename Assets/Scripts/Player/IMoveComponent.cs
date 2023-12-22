using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowChimera
{
    public interface IMoveComponent
    {
        void Init(float speed, float sprintSpeed);

        Vector3 velocity { get; }
        
        bool isGrounded { get; }

        event System.Action onJump;
    }
}
