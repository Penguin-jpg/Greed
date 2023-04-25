using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour // ����
{
    // �����ˮ`
    public int attackDamage = 10;
    // ���h�ĪG
    public Vector2 knockback = Vector2.zero;

    // �u��Ĳ�o�����h
    public Vector2 Knockback
    {
        // �ѩ�w�]�����h�O���k���h�A�ҥH�H�����V����ɭn�����h����V�ϹL��
        get { return transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y); }
    }

    // ��I��O��collider��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        // �p�G��観Damageable����~�|�ͮ�
        if (damageable != null)
        {
            // ���L�Q��
            damageable.Hit(attackDamage, Knockback);
        }
    }
}