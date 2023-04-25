using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour // ���
{
    // ��Slider������
    public Slider healthSlider;
    // ��ܪ���q
    public TMP_Text healthBarText;
    // ���a���i�ˮ`����
    private Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // �]�w��l��
        healthSlider.value = (float)playerDamageable.Health / playerDamageable.MaxHealth;
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        // �⪱�a���i�ˮ`�����q���ܨƥ�ΨӺʬ�onPlayerHealthChanged
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    // ���a��q���ܮɭn������
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        // �קK��q�ܭt��
        newHealth = Mathf.Max(0, newHealth);
        // Slider���Ȥ���0~1
        healthSlider.value = (float)newHealth / maxHealth;
        // ��s��ܦ�q
        healthBarText.text = "HP " + newHealth + " / " + playerDamageable.MaxHealth;
    }
}