using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Core
{
    public class CollisionInfo : MonoBehaviour
    {
        public bool Intersects = false;
        public void OnCollisionEnter(Collision other)
        {
            Intersects = true;
            Debug.Log(other.gameObject.name);
        }

        public void OnCollisionExit(Collision other)
        {
            Intersects = false;
        }
    }
}