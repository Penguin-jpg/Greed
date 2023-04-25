using System.Collections;
using UnityEngine;

public class Enemy : Creature // 怪物的父類別
{
    // 偵測玩家的範圍
    public DetectionZone attackZone;

    // 範圍內是否出現玩家
    protected bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        protected set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    // 攻擊冷卻時間
    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        protected set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }
}