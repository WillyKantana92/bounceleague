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
        
        if(other.gameObject.CompareTag("character"))
        {
            Vector3 dir = Vector3.right;
            switch(direction)
            {
                case Direction.Left:  dir = Vector3.left; break;
                case Direction.Right: dir = Vector3.right; break;
                case Direction.Up:    dir = Vector3.forward; break;
                case Direction.Down:  dir = Vector3.back; break;
            }
            Character player = other.gameObject.GetComponent<Character>();
            player.constantForce.force = dir * force;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger : " + other.gameObject.tag);
        
        if(other.gameObject.CompareTag("character"))
        {
            Character player = other.gameObject.GetComponent<Character>();
            player.constantForce.force = Vector3.zero;
        }
    }
}
