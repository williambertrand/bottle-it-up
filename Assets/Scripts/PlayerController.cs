﻿using System;
using baseclasses;
using UnityEngine;
using extensions;
using util;

public class PlayerController : MonoBehaviorWithInputs
{
    public static PlayerController Instance; 
    private Vector3 _moveInput = Vector3.zero;
    private Rigidbody _rb;
    public NeedleController needleController;
    public float moveSpeed = 3;
 
    private PerlinAxis _monstrosityNoise;
    
    public float MonstrosityLevel { get; private set; } = 1;

    public float BalanceInput { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Can't use this inline because it uses Time.fixedTime
        _monstrosityNoise = new PerlinAxis(0.2f);
        
        _rb = GetComponent<Rigidbody>();
        
        needleController = needleController == null ? GameObjectExtensions.FindComponentByTag<NeedleController>("Needle") : needleController;
        InputActions.Base.Let(i =>
        {
            i.Movement.AddListeners<Vector2>(
                performed: v => _moveInput = v.ToVector3Horizontal(),
                canceled: _ => _moveInput = Vector3.zero
            );

            i.Balance.AddListeners<float>(
                performed: f => BalanceInput = f,
                canceled: _ => BalanceInput = 0
            );
        });
    }

    private void Update()
    {
        var camAdjustedMoveInput = transform.rotation * _moveInput;
        _rb.velocity = camAdjustedMoveInput * moveSpeed;
    }

    private void FixedUpdate()
    {
        // Modify this if we want monstrosity to be calculated in some other way.
        MonstrosityLevel = _monstrosityNoise.GetValue().Squared();
    }
}
