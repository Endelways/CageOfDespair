using System.Security.Cryptography;
using UnityEngine;

namespace Core
{
    public static class PortalExtensions
    {
        private static readonly Quaternion HalfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        private static readonly Vector3 InverseVector = new Vector3() { x = -1, y = 1, z = -1 };
      
        public static void SetMirrorPosition(this Transform target, Transform p1, Transform p2, Transform p3, bool inverse = true)
        {
            var pos = p1.RelativePosition(p2);
            if (inverse)
                pos = pos.Inverse();
            target.SetRelativePosition(p3, pos);
        }

        public static void SetRelativePosition(this Transform target, Transform relativePoint, Vector3 pos)
        {
            var pos2 = relativePoint.position;
            Vector3 newPos = new Vector3() { x = pos2.x + pos.x, y = pos2.y + pos.y, z = pos2.z + pos.z };
            target.position = newPos;
        }
        static void SetRelativeRotation(this Transform target, Transform relativePoint, Quaternion rot)
        {
            var pos2 = relativePoint.rotation;
            Quaternion newRot = new Quaternion() { x = pos2.x + rot.x, y = pos2.y + rot.y, z = pos2.z + rot.z, w = pos2.w + rot.w};
            target.rotation = newRot;
        }
        public static Vector3 RelativePosition(this Transform target, Transform p1)
        {
            var pos1 = target.position;
            var pos2 = p1.position;
            Vector3 relativePos = new Vector3() { x = pos1.x - pos2.x, y = pos1.y - pos2.y, z = pos1.z - pos2.z };
            //relativePos = HalfTurn * relativePos;
            return relativePos;
        }
        static Quaternion RelativeRotation(this Transform target, Transform p1)
        {
            var pos1 = p1.rotation;
            var pos2 = target.rotation;
            Quaternion relativeRot = new Quaternion() { x = pos1.x - pos2.x, y = pos1.y - pos2.y, z = pos1.z - pos2.z, w = pos1.w - pos2.w};
            //relativePos = _halfTurn * relativePos;
            return relativeRot;
        }
        static Vector3 Inverse(this Vector3 vec)
        {
           return new Vector3()
                { x = vec.x * InverseVector.x, y = vec.y * InverseVector.y, z = vec.z * InverseVector.z };
        }
        public static Quaternion Inverse(this Quaternion rotation)
        {
            rotation = HalfTurn * rotation;
            return rotation;
        }
        
        public static void SetMirrorRotation(this Transform target, Transform p1, Transform p2, Transform p3, bool inverse = true)
        {
            var rot = p1.RelativeRotation(p2);
            if (inverse)
            {
                rot = rot.Inverse();
                target.SetRelativeRotation(p3, rot);
            }

            if (!inverse)
            {
                 target.rotation = target.rotation.Inverse();
            }
        }
    }
}