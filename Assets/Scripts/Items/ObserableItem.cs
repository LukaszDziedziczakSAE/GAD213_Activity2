using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserableItem : MonoBehaviour
{
    Rigidbody Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void UseGravity(bool useGravity)
    {
        Rigidbody.useGravity = useGravity;
    }
}
