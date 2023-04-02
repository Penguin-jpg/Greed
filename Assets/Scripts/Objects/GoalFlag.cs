using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalFlag : MonoBehaviour
{
    public MessageText winMessage;
    public MessageText warningText;
    public Damageable boss;
    public float warningTimer = 2f;
    private float timeElapsed = 0f;
    private bool warningTriggered = false;

    private void Update()
    {
        if(warningTriggered)
        {
            if(timeElapsed < warningTimer)
            {
                timeElapsed += Time.deltaTime;
            }else
            {
                warningText.visible = false;
                timeElapsed = 0f;
                warningTriggered = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(boss.IsAlive)
        {
            warningTriggered = true;
            warningText.visible = true;
        }else
        {
            warningTriggered = false;
            warningText.visible = false;
            winMessage.visible = true;
            Destroy(gameObject);
        }
    }
}
