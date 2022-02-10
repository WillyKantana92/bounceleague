using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Collider collider;
    public float destroyInSec;
    public float speed;
    float timerDestroy;
    Vector2 rot;

    GameManager gameManager;

    public void Init(GameManager inGameManager, Vector2 rotation, Quaternion quaternion)
    {
        gameManager = inGameManager;
        
        timerDestroy = destroyInSec;
        rot = rotation;
        transform.localRotation = quaternion;
        
        int goalLength = gameManager.fieldController.goalColliders.Length;
        for(int i = 0; i < goalLength; i++)
        {
            Physics.IgnoreCollision(gameManager.fieldController.goalColliders[i],collider,true);
        }
    }

    void Update()
    {   
        // timerDestroy -= 1 * Time.deltaTime;
        // if(timerDestroy <= 0)
        // {
           //Destroy(this.gameObject);
        // }
    }
    
    void FixedUpdate()
    {
        // Vector3 m = new Vector3(rot.x,0,rot.y);
        // m = m.normalized * speed * Time.deltaTime;
        // rigidbody.MovePosition(transform.position + m);
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision : " + other.gameObject.tag);
        
        if(other.gameObject.CompareTag("ball"))
        {
            ContactPoint contact = other.contacts[0];
            Vector3 point = contact.point;
            other.gameObject.GetComponent<TestBallScript>().ForceBall(point);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
        else if(other.gameObject.CompareTag("character"))
        {
            Destroy(this.gameObject);
        }
    }
}
