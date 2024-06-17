using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserableItem : MonoBehaviour
{
    Rigidbody Rigidbody;
    Collider Collider;

    Vector3 itemOriginalPosition;
    Quaternion itemOriginalRotation;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }

    public void PickUp(Vector3 position)
    {
        itemOriginalPosition = transform.position;
        itemOriginalRotation = transform.rotation;
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.ResetInertiaTensor();
        Rigidbody.useGravity = false;
        Collider.isTrigger = true;
        transform.position = position;
    }

    public void LetGo()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.ResetInertiaTensor();
        Rigidbody.useGravity = true;
        Collider.isTrigger = false;
        transform.position = itemOriginalPosition;
        transform.rotation = itemOriginalRotation;
    }
}
