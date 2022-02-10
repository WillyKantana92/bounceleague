using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaEffector : MonoBehaviour
{
    public Collider collider;
    public float force;
    
    void OnCollisionStay(Collision other)
    {
        Debug.Log("Collision : " + other.gameObject.tag);
        
        if(other.gameObject.CompareTag("character"))
        {
            Character player = other.gameObject.GetComponent<Character>();
            player.rigidBody.AddRelativeForce(Vector3.right * force);
        }
    }
}
