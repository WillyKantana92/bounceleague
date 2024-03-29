﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public Character character;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        character = gameManager.characters[gameManager.characterIndex];
        gameManager.AssignPlayer();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        if(character == null) return;
        character.move = value.ReadValue<Vector2>();
    }
    
    public void OnLook(InputAction.CallbackContext value)
    {
        if(character == null) return;
        character.rotation = value.ReadValue<Vector2>();
    }
}
