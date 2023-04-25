using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour // 漂浮文字，類似楓之谷那樣的數字特效
{
    // 漂浮速度
    public Vector3 floatingSpeed = new Vector3(0, 75, 0);
    // 消失時間
    public float timeToFade = 1f;
    // 文字的位置
    private RectTransform textTransform;
    // TextMeshPro用的東西
    private TextMeshProUGUI textMeshPro;
    private float timeElapsed = 0f;
    // 文字顏色
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
        // 隨著時間慢慢漂浮
        textTransform.position += floatingSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        // 還沒消失前
        if (timeElapsed < timeToFade)
        {
            // 慢慢將文字顏色變淡(不透明度調低)
            float fadeAlpha = textColor.a * (1 - timeElapsed / timeToFade);
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, fadeAlpha);
        }
        else
        {
            // 時間到之後刪掉
            Destroy(gameObject);
        }
    }
}