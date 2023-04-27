using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AirEnemy : Enemy // 空中的敵人
{
    // 飛行速度
    public float flightSpeed = 3f;
    // 由於掉到地板的時候空中的敵人會沉下去，所以要多一個collider
    public BoxCollider2D deathCollider;
    // 飛行類敵人會繞著事先寫好的waypoint飛行
    public GameObject waypointsParent;
    // waypoint偵測距離
    public float waypointReachedDistance = 0.1f;
    // 儲存waypoint
    private List<Transform> waypoints;
    // 目前的waypoint
    private int waypointIndex = 0;
    // 要前往的waypoint
    private Transform targetWaypoint;

    private void Start()
    {
        // 取得除了parent以外的waypoint
        waypoints = waypointsParent.GetComponentsInChildren<Transform>().Where(t => t.transform != waypointsParent.transform).ToList<Transform>();
        // 紀錄要前往的waypoint
        targetWaypoint = waypoints[waypointIndex];
        // 飛行類敵人用不到，所以刪掉
        Destroy(boundaryChecker);
    }

    // 概念和地面敵人一樣
    void Update()
    {
        HasTarget = attackZone.Detected;
        AttackCooldown -= Time.deltaTime;
    }

    // 概念和地面敵人一樣
    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Fly();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Fly()
    {
        // 到waypoint的方向
        Vector2 directionToWaypoint = (targetWaypoint.position - transform.position).normalized;
        // 到waypoint的距離
        float distance = Vector2.Distance(targetWaypoint.position, transform.position);
        rb.velocity = directionToWaypoint * flightSpeed;
        // 更新目前面向方向
        FlipDirection();
        // 離目標waypoint夠進之後就改往下一個waypoint前進
        if (distance <= waypointReachedDistance)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
            targetWaypoint = waypoints[waypointIndex];
        }
    }

    // 更新目前面向方向
    private void FlipDirection()
    {
        // 目前的x方向
        float xDirection = transform.localScale.x;
        // 如果面向右邊但要往左或者面向左邊但要往右時就要轉向
        if ((xDirection > 0 && rb.velocity.x < 0) || (xDirection < 0 && rb.velocity.x > 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    // 死亡時要做的事
    public void OnDeath()
    {
        // 開啟重力(才會有掉落的效果)
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        // 啟動deathCollider避免穿透地面
        deathCollider.enabled = true;
    }
}