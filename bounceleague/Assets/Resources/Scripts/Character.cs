using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public Projectile projectile;
    public Rigidbody rigidBody;
    public Transform launcherPos;
    public float speed;
    public float rotateSpeed;
    public float shootPerSecond;
    InputController inputController;
    Quaternion aimRotation;
    Vector2 move;
    Vector2 rotation;
    Vector2 lastRotation;
    float shootTimer;

    void Awake()
    {
        inputController = new InputController();
        
        inputController.PlayerA.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputController.PlayerA.Move.canceled += ctx => move = Vector2.zero;
        
        inputController.PlayerA.Look.performed += ctx => rotation = ctx.ReadValue<Vector2>();
        inputController.PlayerA.Look.canceled += ctx => rotation = lastRotation;
    }

    void Update()
    {
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }
    
    void FixedUpdate()
    {
        //move - left analog
        Vector3 m = new Vector3(move.x,move.y,0);
        m = m.normalized * speed * Time.deltaTime;
        rigidBody.MovePosition(transform.position + m);

        //rotate - right analog
        lastRotation = rotation;
        float aimAngle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        aimRotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        Quaternion rotate = Quaternion.Slerp(rigidBody.transform.rotation, aimRotation, rotateSpeed * Time.time);
        rigidBody.MoveRotation(rotate);
        
        //shoot
        if(rotation.x > 0.5f || rotation.x < -0.5f)
        {
            Shoot();
        }
        else if(rotation.y > 0.5f || rotation.y < -0.5f)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(shootTimer <= 0)
        {
            Vector3 projectileSpawnPos = launcherPos.position;
            
            Projectile p = Instantiate(projectile.gameObject, projectileSpawnPos, aimRotation).GetComponent<Projectile>();
            p.Init(rotation);

            shootTimer = shootPerSecond;
        }
    }
    
    void OnEnable()
    {
        inputController.PlayerA.Enable();
    }
    
    void OnDisable()
    {
        inputController.PlayerA.Disable();
    }
}
