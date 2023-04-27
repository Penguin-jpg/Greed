using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalFlag : MonoBehaviour // ���I
{
    // �ӧQ�T��
    public MessageText winMessage;
    // ĵ�i�T��
    public MessageText warningMessage;
    // boss���i�ˮ`����(�ΨӹC���O�_����)
    public Damageable boss;
    // ĵ�i��ܮɶ�
    public float warningTimer = 2f;
    // �^��檺�ɶ�
    public float backToMenuTimer = 2f;
    private float timeElapsed = 0f;
    // �O�_Ĳ�oĵ�i
    private bool warningTriggered = false;
    // �O�_�i�H����
    private bool canFinish = false;

    private void Update()
    {
        // Ĳ�oĵ�i���ܴN���ĵ�i�T��2��
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
        // �p�G�i�����C���N��ܦ^���T��2��
        if (canFinish)
        {
            if (timeElapsed >= backToMenuTimer)
            {
                SceneManager.LoadScene("MenuScene");
            }
            timeElapsed += Time.deltaTime;
        }
    }

    // �I����I��Ĳ�o
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // boss�٬��۴N���൲��
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