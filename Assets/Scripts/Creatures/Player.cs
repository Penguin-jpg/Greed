using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(BoundaryChecker), typeof(Damageable))]
public class Player : Creature
{
    public float movingSpeed = 8f; // 移動速度
    public float airMovingSpeed = 5f; // 空中移動速度
    public float jumpImpulse = 9f; // 跳的力道
    public MessageText deadMessage; // 死亡時顯示的文字
    private Vector2 moveInput; // 移動時的輸入
    private float deathTimer = 3f; // 死亡計時器
    private float timeElapsed = 0f;

    // 取得目前的移動速度
    public float CurrentSpeed
    {
        get
        {
            // 可動且在動且沒有撞牆
            if (CanMove && IsMoving && !boundaryChecker.HitWall)
            {
                // 如果在地上就用movingSpeed，在空中就用airMovingSpeed
                return boundaryChecker.IsGrounded ? movingSpeed : airMovingSpeed;
            }
            // 否則不能動
            return 0f;
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    // 是否正在移動
    public bool IsMoving
    {
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
    // 是否面向右邊(用來決定要不要轉向)
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            // 如果轉向了，就把sprite轉向
            if (_isFacingRight != value)
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
        // 如果目前沒有鎖住速度
        if (!damageable.LockVelocity)
        {
            // 移動
            rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        }
        // 紀錄目前是上升還是下降
        animator.SetBool(AnimationStrings.isRising, rb.velocity.y > 0);
    }

    private void Update()
    {
        // 死掉時顯示死亡訊息
        if (!damageable.IsAlive)
        {
            deadMessage.visible = true;
            // 死掉之後過兩秒回到主選單
            if(timeElapsed >= deathTimer)
            {
                SceneManager.LoadScene("MenuScene");
            }
            timeElapsed += Time.deltaTime;
        }
    }

    // 移動時要做的事
    public void OnMove(InputAction.CallbackContext context)
    {
        // 紀錄讀取的輸入
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            // 紀錄是否在移動並將sprite轉向
            IsMoving = (moveInput != Vector2.zero);
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    // 跳躍時要做的事
    public void OnJump(InputAction.CallbackContext context)
    {
        // 有跳且在地上且可移動
        if (context.started && boundaryChecker.IsGrounded && CanMove)
        {
            // 觸發跳躍動畫
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    // 攻擊時要做的事
    public void OnAttack(InputAction.CallbackContext context)
    {
        // 攻擊了
        if (context.started)
        {
            // 觸發攻擊動畫
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    // 調整sprite面向方向
    private void SetFacingDirection(Vector2 moveInput)
    {
        // 輸入往右走卻面向左邊就轉到右邊
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }// 輸入往左邊時卻面向右邊就轉到左邊
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
}