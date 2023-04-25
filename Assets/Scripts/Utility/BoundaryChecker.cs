using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour // �����a�O�B����B�Ѫ�O��Ĳ�I
{
    // �L�o�������G
    public ContactFilter2D castFilter;
    // �P�a�O��Ĳ�I�Z��
    public float groundDistance = 0.05f;
    // �P�����Ĳ�I�Z��
    public float wallDistance = 0.2f;
    // �P�Ѫ�O��Ĳ�I�Z��
    public float ceilingDistance = 0.05f;
    // �����Ϊ�collider
    CapsuleCollider2D touchingCollider;
    Animator animator;
    // Ĳ�I�쪺�a�O
    RaycastHit2D[] touchedGrounds = new RaycastHit2D[5];
    // Ĳ�I�쪺���
    RaycastHit2D[] touchedWalls = new RaycastHit2D[5];
    // Ĳ�I�쪺�Ѫ�O
    RaycastHit2D[] touchedCeilings = new RaycastHit2D[5];

    // �n�ˬd��Ĳ�I��V
    public Vector2 WallCheckingDirection
    {
        get { return gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; }
    }

    private bool _isGrounded;
    // �O�_�b�a�W
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private bool _hitWall;
    // �O�_�b��W�θI����
    public bool HitWall
    {
        get
        {
            return _hitWall;
        }
        private set
        {
            _hitWall = value;
            animator.SetBool(AnimationStrings.hitWall, value);
        }
    }

    private bool _hitCeiling;
    // �O�_�I��Ѫ�O
    public bool HitCeiling
    {
        get
        {
            return _hitCeiling;
        }
        private set
        {
            _hitCeiling = value;
            animator.SetBool(AnimationStrings.hitCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // �C�Ӱ������G���|��castFilter�i��L�o
        // �Ѽ�1:������V, �Ѽ�2:filter, �Ѽ�3:�x�s�}�C, �Ѽ�4:�����Z��
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, touchedGrounds, groundDistance) > 0;
        HitWall = touchingCollider.Cast(WallCheckingDirection, castFilter, touchedWalls, wallDistance) > 0;
        HitCeiling = touchingCollider.Cast(Vector2.up, castFilter, touchedCeilings, ceilingDistance) > 0;
    }
}