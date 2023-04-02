using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageText : MonoBehaviour
{
    public bool visible = false;
    public float destoryTimer = 3f;
    private float timeElapsed = 0f;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.enabled = visible;
    }

    private void Update()
    {
        textMeshPro.enabled = visible;
        if(visible)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= destoryTimer)
            {
                Destroy(gameObject);
            }
        }
    }
}
