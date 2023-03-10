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

    public bool isMoving { get; private set; } // is player moving

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
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
        isMoving = (moveInput != Vector2.zero);
    }
}
