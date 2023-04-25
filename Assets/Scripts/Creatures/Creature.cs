using System.Collections;
using UnityEngine;

// 需要有Rigidbody2D和Damageable才能成功執行
[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class Creature : MonoBehaviour // 生物類型的父類別
{
    protected Rigidbody2D rb;
    protected Animator animator;
    // 用來偵測與牆壁、地面的接觸
    protected BoundaryChecker boundaryChecker;
    // 可傷害實體
    protected Damageable damageable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boundaryChecker = GetComponent<BoundaryChecker>();
        damageable = GetComponent<Damageable>();
    }

    public bool CanMove // 是否能移動
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive // 是否活著
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
    }

    // 被打的時候要擊退
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}