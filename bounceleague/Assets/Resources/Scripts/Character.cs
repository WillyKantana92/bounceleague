using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float speed;
    public float rotateSpeed;
    Vector2 move;
    Vector2 rotation;
    Vector2 lastRotation;
    InputController inputController;

    void Awake()
    {
        inputController = new InputController();
        
        inputController.PlayerA.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputController.PlayerA.Move.canceled += ctx => move = Vector2.zero;
        
        inputController.PlayerA.Look.performed += ctx => rotation = ctx.ReadValue<Vector2>();
        inputController.PlayerA.Look.canceled += ctx => rotation = lastRotation;
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
        Quaternion aimRotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        Quaternion rotate = Quaternion.Slerp(rigidBody.transform.rotation, aimRotation, rotateSpeed * Time.time);
        rigidBody.MoveRotation(rotate);
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
