using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour // �����d���
{
    // �����쪺collider
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    // �d�򤺬O�_�������LCollider
    public bool Detected
    {
        get { return detectedColliders.Count > 0; }
    }

    // �������Lcollider�ɴN�N��[��detectedColliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    // ��collider���}�����d��ɡA�N�N�䲾�XdetectedColliders
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }
}