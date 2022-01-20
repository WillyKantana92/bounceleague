using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffector : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Wall Collision : " + other.gameObject.tag);
        if(other.gameObject.CompareTag("ball"))
        {
            TestBallScript ball = other.gameObject.GetComponent<TestBallScript>();
            Vector3 velocity = ball.ballRb.velocity;
            Vector3 direction = Vector3.Reflect(velocity.normalized, other.contacts[0].normal);
            ball.ballRb.velocity = direction * Mathf.Max(velocity.magnitude, 0f);
        }
    }
}
