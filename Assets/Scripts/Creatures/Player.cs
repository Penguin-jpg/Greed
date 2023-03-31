using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Player : Creature
{
    public float movingSpeed = 8f;
    public float airMovingSpeed = 5f;
    public float jumpImpulse = 9f;
    private Vector2 moveInput;

    public float CurrentSpeed { 
        get
        {
            if(CanMove && IsMoving && !touchingDirections.IsOnWall)
            {
                return touchingDirections.IsGrounded ? movingSpeed : airMovingSpeed;
            }
            return 0f;
        }
    }

    [SerializeField]
    private bool _isMoving = false; // is player moving
    public bool IsMoving { 
        get 
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);

        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight { 
        get 
        { 
            return _isFacingRight;
        }
        private set 
        { 
            if(_isFacingRight != value)
            {
                // flip the local scale to change facing direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        } 
    }

    // This function will be called after a fixed amount of time (default is 0.02s) to compute physics
    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    // Things to do when player moves
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive)
        {
            IsMoving = (moveInput != Vector2.zero);
            SetFacingDirection(moveInput);
        }else
        {
            IsMoving = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }else if(moveInput.x < 0 && IsFacingRight )
        {
            IsFacingRight = false;
        }
    }
}
