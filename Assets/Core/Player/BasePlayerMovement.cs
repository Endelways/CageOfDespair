using System;
using UnityEngine;

namespace Core.Player
{
    public class BasePlayerMovement : MonoBehaviour
    { 
        [SerializeField] protected float speed;
        
        protected Vector3 movementVector;

        protected void Update() => movementVector = transform.right * Input.GetAxis("Horizontal") +
                                                    Input.GetAxis("Vertical") * transform.forward;
        
    }
}