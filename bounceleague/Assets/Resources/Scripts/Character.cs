using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.PlayerLoop;

public class Character : MonoBehaviour
{
    public Projectile projectile;
    public Rigidbody rigidBody;
    public Transform launcherPos;
    public float speed;
    public float rotateSpeed;
    public float shootPerSecond;
    public string padName;
    GameManager gameManager;
    InputController inputController;
    Quaternion aimRotation;
    Vector2 move;
    Vector2 rotation;
    Vector2 lastRotation;
    float shootTimer;

    public void Awake()
    {
        inputController = new InputController();
    
        // inputController.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        // inputController.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        
        // inputController.Gameplay.Look.performed += ctx => rotation = ctx.ReadValue<Vector2>();
        // inputController.Gameplay.Look.canceled += ctx => rotation = lastRotation;
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        move = value.ReadValue<Vector2>();
    }
    
    public void OnLook(InputAction.CallbackContext value)
    {
        rotation = value.ReadValue<Vector2>();
    }

    public void Update()
    {
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    public void CharacterController()
    {
        //move - left analog
        Vector3 m = new Vector3(move.x, 0, move.y);
        m = m.normalized * speed * Time.deltaTime;
        rigidBody.MovePosition(transform.position + m);

        //rotate - right analog
        lastRotation = rotation;
        float aimAngle = Mathf.Atan2(-rotation.y, rotation.x) * Mathf.Rad2Deg;
        aimRotation = Quaternion.AngleAxis(aimAngle, Vector3.up);
        Quaternion rotate = Quaternion.Slerp(rigidBody.transform.rotation, aimRotation, rotateSpeed * Time.time);
        rigidBody.MoveRotation(rotate);

        //shoot
        if(rotation.x > 0.5f ||
           rotation.x < -0.5f)
        {
            Shoot();
        }
        else if(rotation.y > 0.5f ||
                rotation.y < -0.5f)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        CharacterController();
    }

    public void OnEnable()
    {
        inputController.Gameplay.Enable();
    }
    
    public void OnDisable()
    {
        inputController.Gameplay.Disable();
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
}
