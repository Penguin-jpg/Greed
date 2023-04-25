using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour // �}�B��r�A�������������˪��Ʀr�S��
{
    // �}�B�t��
    public Vector3 floatingSpeed = new Vector3(0, 75, 0);
    // �����ɶ�
    public float timeToFade = 1f;
    // ��r����m
    private RectTransform textTransform;
    // TextMeshPro�Ϊ��F��
    private TextMeshProUGUI textMeshPro;
    private float timeElapsed = 0f;
    // ��r�C��
    private Color textColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        // �H�ۮɶ��C�C�}�B
        textTransform.position += floatingSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        // �٨S�����e
        if (timeElapsed < timeToFade)
        {
            // �C�C�N��r�C���ܲH(���z���׽էC)
            float fadeAlpha = textColor.a * (1 - timeElapsed / timeToFade);
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, fadeAlpha);
        }
        else
        {
            // �ɶ��줧��R��
            Destroy(gameObject);
        }
    }
}