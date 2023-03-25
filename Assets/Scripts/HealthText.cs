using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public Vector3 movingSpeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;
    private RectTransform textTransform;
    private TextMeshProUGUI textMeshPro;
    private float timeElapsed = 0f;
    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI > ();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += movingSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if(timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }else
        {
            Destroy(gameObject);
        }
    }
}
