using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerGoalCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Ball")
        {
            TestBallScript ballScript = other.gameObject.GetComponent<TestBallScript>();
            ballScript.OnGoal(transform.position);
        }
    }
}
