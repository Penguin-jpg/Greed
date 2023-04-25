using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour // 偵測範圍用
{
    // 偵測到的collider
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    // 偵測到其他collider時就將其加到detectedColliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    // 當collider離開偵測範圍時，就將其移出detectedColliders
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }
}