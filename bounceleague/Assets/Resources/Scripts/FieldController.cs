using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public Collider[] goalColliders;
    public Collider ballCollider;

    void Start()
    {
        int goalLength = goalColliders.Length;
        for(int i = 0; i < goalLength; i++)
        {
            Physics.IgnoreCollision(goalColliders[i],ballCollider,true);
        }
    }
}
