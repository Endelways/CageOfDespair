using System;
using UnityEngine;

namespace Core.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private bool isSpectating;
        [SerializeField] private bool pointerLock;
        public float sensitivity;
        [SerializeField] private float maxVerticalAngle;
        
        private float rotationX = 0.0f;
        
        private void Start()
        {
            Cursor.lockState = pointerLock ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void Update()
        {
            if (isSpectating)
            {
                var horizontal = Input.GetAxis("Mouse X");
                var vertical = Input.GetAxis("Mouse Y");
                
                transform.parent.Rotate(Vector3.up * horizontal * sensitivity );

                rotationX -= vertical * sensitivity;
                rotationX = Mathf.Clamp(rotationX, -maxVerticalAngle, maxVerticalAngle);
                transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
                
            }
        }
    }
}