using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalFlag : MonoBehaviour
{
    public MessageText winMessage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("WIN!!!");
        winMessage.visible = true;
        Destroy(gameObject);
    }
}
