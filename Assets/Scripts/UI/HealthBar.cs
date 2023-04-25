using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour // 血條
{
    // 用Slider做橫的血條
    public Slider healthSlider;
    // 顯示的血量
    public TMP_Text healthBarText;
    // 玩家的可傷害實體
    private Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 設定初始值
        healthSlider.value = (float)playerDamageable.Health / playerDamageable.MaxHealth;
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        // 把玩家的可傷害實體血量改變事件用來監看onPlayerHealthChanged
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    // 玩家血量改變時要做的事
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        // 避免血量變負數
        newHealth = Mathf.Max(0, newHealth);
        // Slider的值介於0~1
        healthSlider.value = (float)newHealth / maxHealth;
        // 更新顯示血量
        healthBarText.text = "HP " + newHealth + " / " + playerDamageable.MaxHealth;
    }
}