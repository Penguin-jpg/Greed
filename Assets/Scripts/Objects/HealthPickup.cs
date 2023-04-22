using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 15;
    private AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable)
        {
            damageable.Heal(healthRestore);
            if(pickupSource)
            {
                SoundManager.PlaySound("heal");
            }
            Destroy(gameObject);
        }
    }
}
