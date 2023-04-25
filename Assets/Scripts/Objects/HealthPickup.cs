using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour // �ߤF�i�H�^�媺�F��
{
    // �^��q
    public int healthRestore = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        // �p�G�I�쪺�F�観Damageable����~��^��
        if (damageable != null)
        {
            // �^��
            damageable.Heal(healthRestore);
            // ������
            SoundManager.PlaySound("heal");
            // �R������
            Destroy(gameObject);
        }
    }
}