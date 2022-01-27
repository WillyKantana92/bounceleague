using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public float stunTime;
    public string padName;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 rotation;
    GameManager gameManager;
    InputController inputController;
    Quaternion aimRotation;
    Vector2 lastRotation;
    float shootTimer;
    float stunTimer;
    bool isStun;

    Vector3 initPos;
    Quaternion initRot;

    public void Awake()
    {
        inputController = new InputController();
        initPos = transform.position;
        initRot = transform.rotation;

        // inputController.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        // inputController.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        // inputController.Gameplay.Look.performed += ctx => rotation = ctx.ReadValue<Vector2>();
        // inputController.Gameplay.Look.canceled += ctx => rotation = lastRotation;
    }

    public void Init(GameManager gameManagerIn)
    {
        gameManager = gameManagerIn;
    }

    public void ResetChar()
    {
        transform.position = initPos;
        transform.rotation = initRot;
    }

    public void Update()
    {
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        if(isStun && stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;

            if(stunTimer <= 0)
            {
                isStun = false;
            }
        }
    }

    void CharacterController()
    {
        if(!isStun)
        {
            //move - left analog
            Vector3 m = new Vector3(move.x, 0, move.y);
            m = m.normalized * (speed * Time.fixedDeltaTime);
            rigidBody.MovePosition(transform.position + m);

            // transform.position += m;

            //rotate - right analog
            lastRotation = rotation;
            if(rotation.x != 0 ||
               rotation.y != 0)
            {
                float aimAngle = Mathf.Atan2(-rotation.y, rotation.x) * Mathf.Rad2Deg;
                aimRotation = Quaternion.AngleAxis(aimAngle, Vector3.up);
                Quaternion rotate = Quaternion.RotateTowards(transform.localRotation, aimRotation, rotateSpeed);
                rigidBody.MoveRotation(rotate);

                // transform.localRotation = rotate;
            }

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
    }

    void FixedUpdate()
    {
        if(gameManager == null) return;
        if(gameManager.currentGameState != GameState.Gameplay) return;
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
            p.Init(rotation, transform.localRotation);

            shootTimer = shootPerSecond;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("ball"))
        {
            isStun = true;
            stunTimer = stunTime;
        }
    }
}
