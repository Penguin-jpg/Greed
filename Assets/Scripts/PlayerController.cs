using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] // Rigidbody2D has to exist
public class PlayerController : MonoBehaviour
{
    public float movingSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

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
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function will be called after a fixed amount of time (default is 0.02s) to compute physics
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * movingSpeed, rb.velocity.y);
    }

    // Things to do when player moves
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = (moveInput != Vector2.zero);
        SetFacingDirection(moveInput);
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
