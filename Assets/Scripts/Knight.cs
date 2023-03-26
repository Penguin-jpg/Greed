using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float movingAcceleration = 300f;
    public float maxSpeed = 3f;
    public float moveStopRate = 0.05f;
    public DetectionZone attackZone, cliffDetectionZone;
    private Rigidbody2D rb;
    private TouchingDirections touchingDirecitons;
    private Animator animator;
    private Damageable damageable;
    public enum WalkableDirection { Right, Left };

    private WalkableDirection _walkDirection;
    private Vector2 walkDriectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if(_walkDirection != value)
            {
                // flip
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    walkDriectionVector = Vector2.right;
                }else
                {
                    walkDriectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    private bool _hasTarget = false;
    public bool HasTarget {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public float AttackCooldown {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set 
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirecitons = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    { 
        HasTarget = attackZone.detectedColliders.Count > 0;
        AttackCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(touchingDirecitons.IsGrounded && touchingDirecitons.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (movingAcceleration * walkDriectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), 
                    rb.velocity.y
                );
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, moveStopRate), rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }else
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if(touchingDirecitons.IsGrounded)
        {
            FlipDirection();
        }
    }
}
