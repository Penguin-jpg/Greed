using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour // �i�ˮ`����
{
    // �Q�����Ĳ�o���ƥ�(���h)
    public UnityEvent<int, Vector2> damageableKnockback;
    // ���`��Ĳ�o���ƥ�
    public UnityEvent damageableDeath;
    // ��q���ܮ�Ĳ�o���ƥ�
    public UnityEvent<int, int> healthChanged;
    private Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    // �̤j��q
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
    // �ثe��q
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            // ��q���ܾɭP�ƥ�Ĳ�o
            healthChanged?.Invoke(_health, MaxHealth);
            // ���`
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
            // ���`�ɭP�ƥ�Ĳ�o
            if (value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }

    // �O�_��w�t��(�Ҧp�Q����)
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    [SerializeField]
    private bool isInvincible = false; // �O�_�L��
    private float timeSinceHit = 0f;
    public float invincibilityTime = 1f; // �L��timer

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ����L�Įɶ�
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

    // �Q���ɭn������
    public void Hit(int damage, Vector2 knockback)
    {
        // �٬��ۥB�D�L��
        if (IsAlive && !isInvincible)
        {
            // �Q���ɦ���öi�J�L�Ī��A
            Health -= damage;
            isInvincible = true;
            // Ĳ�o�Q���ʵe
            animator.SetTrigger(AnimationStrings.hitTrigger);
            // ���t��
            LockVelocity = true;
            // Ĳ�o�ƥ�(���h)
            damageableKnockback?.Invoke(damage, knockback);
            // Ĳ�o��ܶˮ`�Ʀr���ƥ�
            CreatureEvents.creatureDamaged.Invoke(gameObject, damage);
        }
    }

    // �ɦ�ɭn������
    public void Heal(int healthRestore)
    {
        if (IsAlive)
        {
            // �̤j�ɦ�q(���|�ɶW�L�̤j��q)
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            // ��ڸɦ�q(����ɶW�L�̤j�ɦ�q)
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            // Ĳ�o��ܸɦ�Ʀr���ƥ�
            CreatureEvents.creatureHealed.Invoke(gameObject, actualHeal);
        }
    }
}