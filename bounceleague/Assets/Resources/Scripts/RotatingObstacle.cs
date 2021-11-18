using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    public float rotationSpeed = 1;
    public bool rotatingClockWise;

    Vector3 rot;
    int dir;
    
    // Start is called before the first frame update
    void Start()
    {
        dir = rotatingClockWise ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        rot = Vector3.forward * (rotationSpeed * dir * Time.deltaTime * 100f);
        transform.Rotate(rot);
    }
}
