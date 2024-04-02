using System;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsPlayerMovement : BasePlayerMovement
    {
        private new Rigidbody rigidbody;
        private void Start() => rigidbody = GetComponent<Rigidbody>();
        private void FixedUpdate() =>
            rigidbody.MovePosition(transform.position + movementVector * speed * Time.fixedDeltaTime);
    }
}