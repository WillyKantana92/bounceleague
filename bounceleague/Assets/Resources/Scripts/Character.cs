using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float speed;
    Vector2 move;
    InputController inputController;

    void Awake()
    {
        inputController = new InputController();
        
        inputController.PlayerA.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputController.PlayerA.Move.canceled += ctx => move = Vector2.zero;
        
        inputController.PlayerA.Look.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputController.PlayerA.Look.canceled += ctx => move = Vector2.zero;
    }

    void FixedUpdate()
    {
        Vector3 m = new Vector3(move.x,move.y,0);
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
