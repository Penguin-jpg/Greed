using System.Collections;
using UnityEngine;

public class Enemy : Creature
{
    public DetectionZone attackZone;

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