using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalFlag : MonoBehaviour // 終點
{
    // 勝利訊息
    public MessageText winMessage;
    // 警告訊息
    public MessageText warningMessage;
    // boss的可傷害實體(用來遊戲是否結束)
    public Damageable boss;
    // 警告顯示時間
    public float warningTimer = 2f;
    // 回選單的時間
    public float backToMenuTimer = 2f;
    private float timeElapsed = 0f;
    // 是否觸發警告
    private bool warningTriggered = false;
    // 是否可以結束
    private bool canFinish = false;

    private void Update()
    {
        // 觸發警告的話就顯示警告訊息2秒
        if (warningTriggered)
        {
            if (timeElapsed < warningTimer)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                warningMessage.visible = false;
                timeElapsed = 0f;
                warningTriggered = false;
            }
        }
        // 如果可結束遊戲就顯示回選單訊息2秒
        if (canFinish)
        {
            if (timeElapsed >= backToMenuTimer)
            {
                SceneManager.LoadScene("MenuScene");
            }
            timeElapsed += Time.deltaTime;
        }
    }

    // 碰到終點時觸發
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // boss還活著就不能結束
        if (boss.IsAlive)
        {
            warningTriggered = true;
            warningMessage.visible = true;
        }
        else
        {
            warningTriggered = false;
            warningMessage.visible = false;
            winMessage.visible = true;
            canFinish = true;
            timeElapsed = 0f;
        }
    }
}