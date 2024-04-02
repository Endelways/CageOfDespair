using System;
using UnityEngine;

namespace Core
{
    public class BlockInteractableObject : MonoBehaviour, IInteractableObject, ISizeableObject
    {
        public void Interaction(Hand hand)
        {
            hand.Item = transform.gameObject;
        }
    }
}