using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour // 撿了可以回血的東西
{
    // 回血量
    public int healthRestore = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        // 如果碰到的東西有Damageable物件才能回血
        if (damageable != null)
        {
            // 回血
            damageable.Heal(healthRestore);
            // 播音效
            SoundManager.PlaySound("heal");
            // 刪掉物件
            Destroy(gameObject);
        }
    }
}