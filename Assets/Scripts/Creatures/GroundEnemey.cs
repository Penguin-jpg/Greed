using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoundaryChecker), typeof(Damageable))]
public class GroundEnemey : Enemy // 地面上的怪物
{
    // 移動的加速度(從慢加速到快)
    public float movingAcceleration = 300f;
    // 最大速度(避免衝太快)
    public float maxSpeed = 3f;
    // 減速時的比例
    public float moveStopRate = 0.05f;
    // 地面移動時必須考慮是否會走出邊緣
    public DetectionZone edegDetectionZone;
    // 移動方向
    public Vector2 movingDirection = Vector2.right;

    // 邏輯和玩家操作大致相同，只是改成自我判定前進方向
    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
                movingDirection *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 攻擊範圍內是否有偵測到玩家
        HasTarget = attackZone.Detected;
        // 攻擊開始進入冷卻
        AttackCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // 在地上且碰到牆就轉向
        if (boundaryChecker.IsGrounded && boundaryChecker.HitWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                // 移動的速度限制在-maxSpeed和maxSpeed之間
                // 且移動時是慢慢加速
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (movingAcceleration * movingDirection.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed),
                    rb.velocity.y
                );
            }
            else
            {
                // 不動時將速度慢慢減到0
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, moveStopRate), rb.velocity.y);
            }
        }
    }

    // 翻轉面向方向
    private void FlipDirection()
    {
        IsFacingRight = !IsFacingRight;
    }

    // 偵測到陸地邊界時要做的事
    public void OnCliffDetected()
    {
        // 在地上碰到邊界要轉向
        if (boundaryChecker.IsGrounded)
        {
            FlipDirection();
        }
    }
}