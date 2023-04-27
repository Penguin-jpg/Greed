using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour // 可傷害實體
{
    // 被打到時觸發的事件(擊退)
    public UnityEvent<int, Vector2> damageableKnockback;
    // 死亡時觸發的事件
    public UnityEvent damageableDeath;
    // 血量改變時觸發的事件
    public UnityEvent<int, int> healthChanged;
    private Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    // 最大血量
    public int MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;
    // 目前血量
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            // 血量改變導致事件觸發
            healthChanged?.Invoke(_health, MaxHealth);
            // 死亡
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            // 死亡導致事件觸發
            if (value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }

    // 是否鎖定速度(例如被打時)
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    [SerializeField]
    private bool isInvincible = false; // 是否無敵
    private float timeSinceHit = 0f;
    public float invincibilityTime = 1f; // 無敵timer

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 遞減無敵時間
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                timeSinceHit = 0f;
                isInvincible = false;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    // 被打時要做的事
    public void Hit(int damage, Vector2 knockback)
    {
        // 還活著且非無敵
        if (IsAlive && !isInvincible)
        {
            // 被打時扣血並進入無敵狀態
            Health -= damage;
            isInvincible = true;
            // 觸發被打動畫
            animator.SetTrigger(AnimationStrings.hitTrigger);
            // 鎖住速度
            LockVelocity = true;
            // 觸發事件(擊退)
            damageableKnockback?.Invoke(damage, knockback);
            // 觸發顯示傷害數字的事件
            CreatureEvents.creatureDamaged.Invoke(gameObject, damage);
        }
    }

    // 補血時要做的事
    public void Heal(int healthRestore)
    {
        if (IsAlive)
        {
            // 最大補血量(不會補超過最大血量)
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            // 實際補血量(不能補超過最大補血量)
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            // 觸發顯示補血數字的事件
            CreatureEvents.creatureHealed.Invoke(gameObject, actualHeal);
        }
    }
}