using System.Collections;
using System.Collections.Generic;
using baseclasses;
using UnityEngine;
using extensions;

public class PlayerController : MonoBehaviorWithInputs
{
    private Vector3 _moveInput = Vector3.zero;
    private Rigidbody _rb;
    public float moveSpeed = 3;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        InputActions.Base.Let(i =>
        {
            i.Movement.AddListeners<Vector2>(
                performed: v => _moveInput = v.ToVector3Horizontal(),
                canceled: _ => _moveInput = Vector3.zero
            );
        });
    }
    
    
    private void Update()
    {
        var camAdjustedMoveInput = transform.rotation * _moveInput;
        _rb.velocity = camAdjustedMoveInput * moveSpeed;
    }
}
