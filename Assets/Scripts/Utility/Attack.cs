using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour // 攻擊
{
    // 攻擊傷害
    public int attackDamage = 10;
    // 擊退效果
    public Vector2 knockback = Vector2.zero;

    // 真正觸發的擊退
    public Vector2 Knockback
    {
        // 由於預設的擊退是往右擊退，所以人物面向左邊時要把擊退的方向反過來
        get { return transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y); }
    }

    // 當碰到別的collider時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        // 如果對方有Damageable物件才會生效
        if (damageable != null)
        {
            // 讓他被打
            damageable.Hit(attackDamage, Knockback);
        }
    }
}