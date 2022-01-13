using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyInSec;
    public float speed;
    float timerDestroy;
    Vector2 rot;

    public void Init(Vector2 rotation, Quaternion quaternion)
    {
        timerDestroy = destroyInSec;
        rot = rotation;
        transform.localRotation = quaternion;
    }

    void Update()
    {
        timerDestroy -= 1 * Time.deltaTime;
        if(timerDestroy <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    void FixedUpdate()
    {
        // Vector3 m = new Vector3(rot.x,0,rot.y);
        // m = m.normalized * speed * Time.deltaTime;
        // rigidbody.MovePosition(transform.position + m);
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
    }
}
