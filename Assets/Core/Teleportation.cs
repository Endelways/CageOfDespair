using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Portal Portal;
    private Vector3? lastObjectPosition;
    [SerializeField] private bool enableTeleportation;
    void Start()
    {
        if (Portal.Teleportation != this)
        {
            throw new NotImplementedException();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enableTeleportation)
        {
            Vector3 portalToPlayer = other.transform.position - Portal.Frame.transform.position;
            float dotProduct = Vector3.Dot(Portal.Frame.transform.up, portalToPlayer);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                Teleport(other.transform);
            }
            // var frameTransform = Portal.Frame.transform;
            // if (lastObjectPosition == null)
            //     lastObjectPosition = other.transform.RelativePosition(Portal.Frame.transform);
            // else
            // {
            //     var curObjectPosition = other.transform.RelativePosition(Portal.Frame.transform);
            //     if (Vector3.Distance((Vector3)lastObjectPosition, frameTransform.position) >
            //         Vector3.Distance(curObjectPosition, frameTransform.position))
            //     {
            //         Teleport(other.transform);
            //         lastObjectPosition = null;   
            //     }
            //     lastObjectPosition = curObjectPosition;
            // }
             
            //float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;
            //if (zPos < 0) Teleport(other.transform);
        }
    }

    private void Teleport(Transform obj)
    {
        // Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        // //localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        // obj.position = Portal.ConnectedPortal.transform.GetComponentInParent<Transform>().localToWorldMatrix.MultiplyPoint3x4(localPos);
        //
        // Quaternion difference = Portal.ConnectedPortal.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 0, 0));
        // obj.rotation = difference * obj.rotation;
        // Position
        // Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        // localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        // obj.position = Portal.ConnectedPortal.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        // Rotation
        // Quaternion difference = Portal.ConnectedPortal.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        // obj.rotation = difference * obj.rotation;
        
       // obj.MirrorPosition(transform, Portal.ConnectedPortal.transform);
       //obj.SetMirrorPosition(obj,Portal.Frame.transform, Portal.ConnectedPortal.Frame.transform, false);
       obj.SetMirrorPosition(obj, Portal.Frame.transform, Portal.ConnectedPortal.Frame.transform);
       obj.rotation = obj.rotation.Inverse();
       //obj.SetMirrorRotation(obj,Portal.Frame.transform, Portal.ConnectedPortal.Frame.transform, false);
    }
}
