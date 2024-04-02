using System;
using Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Hand : MonoBehaviour
{
    public KeyCode keyCode;
    public Transform Camera;
    public GameObject Player;
    public IndicatorObject Indicator;
    private float pickUpedDistance, pickUpedScale, pickUpedMass;
    public LayerMask objectLayer, noObjectLayerMask, playerLayerMask;
    public float MaxDistance; // hand length
    public GameObject Item;
    public float sizeMultipyer = 0;
    public static Hand PlayerHand;
    public Joint joint;
    public void Awake()
    {
        if (PlayerHand == null)
        {
            PlayerHand = this;
        }
        else
        {
            Destroy(this);
        }

        joint = GetComponent<Joint>();
    }

    public void Update()
    {
        if (Item != null)
        {
            if (Item.GetComponent<ISizeableObject>() != null)
            {
                ReSizeItem();
            }
            if (Input.GetKeyDown(keyCode))
            {
                joint.connectedBody = null;
                Physics.IgnoreCollision(Player.GetComponent<Collider>(), Item.GetComponent<Collider>(), false);
                Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Item = null;
            }
        }
        else
        {
            if (Input.GetKeyDown(keyCode))
            {
                CheckItemForInteract();
            }
        }
            
    }
    
    void CheckItemForInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.position, Camera.forward, out hit, MaxDistance, objectLayer))
        {
            var interactComponent = hit.collider.GetComponent<IInteractableObject>();
            var rigidBodyComponent = hit.collider.GetComponent<Rigidbody>();
            var colliderComponent = hit.collider.GetComponent<Collider>();
            ;
            if (interactComponent != null)
            {
                Indicator.enabled = true;
                Indicator.textField.text = "Press " + keyCode.ToString() + " To Interact";
                if (Input.GetKeyDown(keyCode))
                {
                    var position = rigidBodyComponent.transform.position;
                    pickUpedDistance = Vector3.Distance(Camera.transform.position,position);
                    pickUpedScale = rigidBodyComponent.transform.localScale.x;
                    pickUpedMass = rigidBodyComponent.mass;
                    interactComponent.Interaction(this);
                    joint.transform.position = position;
                    joint.connectedBody = rigidBodyComponent;
                    rigidBodyComponent.constraints = RigidbodyConstraints.FreezeRotation;
                    Physics.IgnoreCollision(Player.GetComponent<Collider>(), Item.GetComponent<Collider>(), true);
                }

            }
            else
            {
                Indicator.enabled = false;
            }
        }
        else
        {
            Indicator.enabled = false;
        }
    }

    void ReSizeItem()
    {
        RaycastHit noObjectHit;
        if (Physics.Raycast(Camera.position, Camera.forward, out noObjectHit, Mathf.Infinity, noObjectLayerMask))
        {
            var positionOffset = Camera.forward * Item.transform.localScale.x;

            Item.transform.localPosition = noObjectHit.point - positionOffset;

            float distance = Vector3.Distance(Camera.position, Item.transform.position);
            float scaleMultiplier = distance / pickUpedDistance;
            Vector3 newVector = scaleMultiplier * pickUpedScale * Vector3.one;
            Item.transform.localScale = newVector != Vector3.zero ? newVector : Vector3.one;
            Item.GetComponent<Rigidbody>().mass = scaleMultiplier > 0 ? pickUpedMass * scaleMultiplier : 1;

        }
    }
}
