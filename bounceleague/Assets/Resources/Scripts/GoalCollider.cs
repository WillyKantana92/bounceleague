using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    public float gravityPullForce = 200f;
    public static float gravityRadius = 1f;
    
    void Awake()
    {
        gravityRadius = GetComponent<CapsuleCollider>().radius;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Attract objects towards an area when they come within the bounds of a collider.
    /// This function is on the physics timer so it won't necessarily run every frame.
    /// </summary>
    /// <param name="other">Any object within reach of gravity's collider</param>
    void OnTriggerStay(Collider other)
    {
        if(other.name == "Ball" && other.attachedRigidbody)
        {
            Vector3 goalPos = transform.position;
            Vector3 ballPos = other.transform.position;
            float gravityIntensity = Vector3.Distance(goalPos, ballPos) / gravityRadius;
            Vector3 appliedForce = (goalPos - ballPos) * gravityIntensity * other.attachedRigidbody.mass * gravityPullForce * Time.smoothDeltaTime;
            other.attachedRigidbody.AddForce(appliedForce);
            Debug.DrawRay(ballPos, goalPos - ballPos);
        }
    }
}
