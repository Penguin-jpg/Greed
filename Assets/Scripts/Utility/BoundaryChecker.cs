using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour // 偵測地板、牆壁、天花板的觸碰
{
    // 過濾偵測結果
    public ContactFilter2D castFilter;
    // 與地板的觸碰距離
    public float groundDistance = 0.05f;
    // 與牆壁的觸碰距離
    public float wallDistance = 0.2f;
    // 與天花板的觸碰距離
    public float ceilingDistance = 0.05f;
    // 偵測用的collider
    CapsuleCollider2D touchingCollider;
    Animator animator;
    // 觸碰到的地板
    RaycastHit2D[] touchedGrounds = new RaycastHit2D[5];
    // 觸碰到的牆壁
    RaycastHit2D[] touchedWalls = new RaycastHit2D[5];
    // 觸碰到的天花板
    RaycastHit2D[] touchedCeilings = new RaycastHit2D[5];

    // 要檢查的觸碰方向
    public Vector2 WallCheckingDirection
    {
        get { return gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; }
    }

    private bool _isGrounded;
    // 是否在地上
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
    // 是否在牆上或碰到牆
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
    // 是否碰到天花板
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
        // 每個偵測結果都會由castFilter進行過濾
        // 參數1:偵測方向, 參數2:filter, 參數3:儲存陣列, 參數4:偵測距離
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, touchedGrounds, groundDistance) > 0;
        HitWall = touchingCollider.Cast(WallCheckingDirection, castFilter, touchedWalls, wallDistance) > 0;
        HitCeiling = touchingCollider.Cast(Vector2.up, castFilter, touchedCeilings, ceilingDistance) > 0;
    }
}