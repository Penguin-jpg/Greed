using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AirEnemy : Enemy
{
    public float flightSpeed = 3f;
    public BoxCollider2D deathCollider;
    public GameObject waypointsParent;
    public float waypointReachedDistance = 0.1f;
    private List<Transform> waypoints;
    private int waypointIndex = 0;
    private Transform nextWaypoint;

    private void Start()
    {
        // get tranforms of children without getting the parent transform
        waypoints = waypointsParent.GetComponentsInChildren<Transform>().Where(t => t.transform != waypointsParent.transform).ToList<Transform>();
        nextWaypoint = waypoints[waypointIndex];
        Destroy(touchingDirections); // destroy this since FlyingEye won't use it
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        AttackCooldown -= Time.deltaTime;
    }

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
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();
        if (distance <= waypointReachedDistance)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
            nextWaypoint = waypoints[waypointIndex];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if (localScale.x > 0 && rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
        else if (localScale.x < 0 && rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
    }

    public void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}