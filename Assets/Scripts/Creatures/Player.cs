using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(BoundaryChecker), typeof(Damageable))]
public class Player : Creature
{
    public float movingSpeed = 8f; // ���ʳt��
    public float airMovingSpeed = 5f; // �Ť����ʳt��
    public float jumpImpulse = 9f; // �����O�D
    public MessageText deadMessage; // ���`����ܪ���r
    private Vector2 moveInput; // ���ʮɪ���J
    private float deathTimer = 3f; // ���`�p�ɾ�
    private float timeElapsed = 0f;

    // ���o�ثe�����ʳt��
    public float CurrentSpeed
    {
        get
        {
            // �i�ʥB�b�ʥB�S������
            if (CanMove && IsMoving && !boundaryChecker.HitWall)
            {
                // �p�G�b�a�W�N��movingSpeed�A�b�Ť��N��airMovingSpeed
                return boundaryChecker.IsGrounded ? movingSpeed : airMovingSpeed;
            }
            // �_�h�����
            return 0f;
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    // �O�_���b����
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
    // �O�_���V�k��(�ΨӨM�w�n���n��V)
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            // �p�G��V�F�A�N��sprite��V
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
        // �p�G�ثe�S�����t��
        if (!damageable.LockVelocity)
        {
            // ����
            rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        }
        // �����ثe�O�W���٬O�U��
        animator.SetBool(AnimationStrings.isRising, rb.velocity.y > 0);
    }

    private void Update()
    {
        // ��������ܦ��`�T��
        if (!damageable.IsAlive)
        {
            deadMessage.visible = true;
            // ��������L���^��D���
            if(timeElapsed >= deathTimer)
            {
                SceneManager.LoadScene("MenuScene");
            }
            timeElapsed += Time.deltaTime;
        }
    }

    // ���ʮɭn������
    public void OnMove(InputAction.CallbackContext context)
    {
        // ����Ū������J
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            // �����O�_�b���ʨñNsprite��V
            IsMoving = (moveInput != Vector2.zero);
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    // ���D�ɭn������
    public void OnJump(InputAction.CallbackContext context)
    {
        // �����B�b�a�W�B�i����
        if (context.started && boundaryChecker.IsGrounded && CanMove)
        {
            // Ĳ�o���D�ʵe
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    // �����ɭn������
    public void OnAttack(InputAction.CallbackContext context)
    {
        // �����F
        if (context.started)
        {
            // Ĳ�o�����ʵe
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    // �վ�sprite���V��V
    private void SetFacingDirection(Vector2 moveInput)
    {
        // ��J���k���o���V����N���k��
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }// ��J������ɫo���V�k��N��쥪��
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
}