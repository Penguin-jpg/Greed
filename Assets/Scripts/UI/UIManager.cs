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
        CreatureEvents.creatureDamaged += CreatureDamaged;
        CreatureEvents.creatureHealed += CreatureHealed;
    }

    private void OnDisable()
    {
        CreatureEvents.creatureDamaged -= CreatureDamaged;
        CreatureEvents.creatureHealed -= CreatureHealed;
    }

    // �ͪ����ˮ�Ĳ�o
    public void CreatureDamaged(GameObject creature, int damageReceived)
    {
        // �ھڥͪ���m�M�w�}�B��r�ͦ���m
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(creature.transform.position);
        // �ͦ��}�B��r
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    // �ͪ��ɦ��Ĳ�o
    public void CreatureHealed(GameObject creature, int healthRestored)
    {
        // ��������˪�function�ۦP
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(creature.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
}