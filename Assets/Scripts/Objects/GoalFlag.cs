using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalFlag : MonoBehaviour
{
    public MessageText winMessage;
    public MessageText warningText;
    public Damageable boss;
    public float warningTimer = 2f;
    public float backToMenuTimer = 2f;
    private float timeElapsed = 0f;
    private bool warningTriggered = false;
    private bool canFinish = false;

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
        if(canFinish)
        {
            if(timeElapsed >= backToMenuTimer)
            { 
                SceneManager.LoadScene("MenuScene");
            }
            timeElapsed += Time.deltaTime;
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
            canFinish = true;
            timeElapsed = 0f;
        }
    }
}
