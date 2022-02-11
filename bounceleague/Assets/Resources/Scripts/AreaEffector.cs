using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left, Right, Up, Down
}

public class AreaEffector : MonoBehaviour
{
    public Direction direction;
    public float force;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger : " + other.gameObject.tag);
        
        if(other.gameObject.CompareTag("ball"))
        {
            Vector3 dir = Vector3.right;
            switch(direction)
            {
                case Direction.Left:  dir = Vector3.left; break;
                case Direction.Right: dir = Vector3.right; break;
                case Direction.Up:    dir = Vector3.forward; break;
                case Direction.Down:  dir = Vector3.back; break;
            }
            TestBallScript ball = other.gameObject.GetComponent<TestBallScript>();
            ball.constantForceModifier.force = dir * force;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger : " + other.gameObject.tag);
        
        if(other.gameObject.CompareTag("ball"))
        {
            TestBallScript ball = other.gameObject.GetComponent<TestBallScript>();
            ball.constantForceModifier.force = Vector3.zero;
        }
    }
}
