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
        CharacterEvents.characterDamaged += ChracterDamaged;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= ChracterDamaged;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    // 角色受傷時觸發
    public void ChracterDamaged(GameObject character, int damageReceived)
    {
        // 根據玩家位置決定漂浮文字生成位置
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // 生成漂浮文字
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    // 角色補血時觸發
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // 概念跟受傷的function相同
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
}