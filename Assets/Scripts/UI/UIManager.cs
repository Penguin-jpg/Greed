using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // 傷害FloatingText和補血FloatingText
    public GameObject damageTextPrefab, healthTextPrefab;
    // 遊戲的Canvas
    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        // 加入監聽
        CreatureEvents.creatureDamaged += CreatureDamaged;
        CreatureEvents.creatureHealed += CreatureHealed;
    }

    private void OnDisable()
    {
        CreatureEvents.creatureDamaged -= CreatureDamaged;
        CreatureEvents.creatureHealed -= CreatureHealed;
    }

    // 生物受傷時觸發
    public void CreatureDamaged(GameObject creature, int damageReceived)
    {
        // 根據生物位置決定漂浮文字生成位置
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(creature.transform.position);
        // 生成漂浮文字
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    // 生物補血時觸發
    public void CreatureHealed(GameObject creature, int healthRestored)
    {
        // 概念跟受傷的function相同
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(creature.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
}