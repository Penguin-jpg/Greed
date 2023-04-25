using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // �ˮ`FloatingText�M�ɦ�FloatingText
    public GameObject damageTextPrefab, healthTextPrefab;
    // �C����Canvas
    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        // �[�J��ť
        CharacterEvents.characterDamaged += ChracterDamaged;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= ChracterDamaged;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    // ������ˮ�Ĳ�o
    public void ChracterDamaged(GameObject character, int damageReceived)
    {
        // �ھڪ��a��m�M�w�}�B��r�ͦ���m
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // �ͦ��}�B��r
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    // ����ɦ��Ĳ�o
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // ��������˪�function�ۦP
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
}