using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallScript : MonoBehaviour
{
    public Rigidbody ballRb;

    int force = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ballRb.AddForce(Vector3.left * force);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ballRb.AddForce(Vector3.back * force);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ballRb.AddForce(Vector3.forward * force);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ballRb.AddForce(Vector3.right * force);
        }
    }
}
