using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleAI : MonoBehaviour
{
    public enum states
    {
        loop,
        backAndForth
    }
    public states currentState = states.backAndForth;
    private Vector2 targetPosition;
    private int currentWaypoint;
    private bool reverseOrder;
    [Tooltip("Modify the movement speed of the GameObject")]
    public float speedMod = 1.0f;
    [Tooltip("array of points the npc will travel through")]
    public Transform[] waypoints;

    Rigidbody2D rb;

    private void Start()
    {
        targetPosition = waypoints[0].position;

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.position = Vector2.MoveTowards(rb.position, targetPosition, Time.deltaTime * 1.3f * speedMod);

        switch (currentState)
        {
            case states.loop:
                if (Vector2.Distance(rb.position, targetPosition) < float.Epsilon)
                {
                    targetPosition = waypoints[currentWaypoint].position;
                    currentWaypoint++;
                    if (currentWaypoint == waypoints.Length)
                    {
                        currentWaypoint = 0;
                    }
                }
                break;
            case states.backAndForth:
                if (Vector2.Distance(rb.position, targetPosition) < float.Epsilon && currentWaypoint < waypoints.Length && !reverseOrder)
                {
                    currentWaypoint++;
                    if (currentWaypoint == waypoints.Length)
                    {
                        reverseOrder = true;
                        currentWaypoint -= 2;
                    }
                    targetPosition = waypoints[currentWaypoint].position;
                }else if (Vector2.Distance(rb.position, targetPosition) < float.Epsilon && currentWaypoint > 0 && reverseOrder)
                {
                    currentWaypoint--;
                    if (currentWaypoint == 0)
                    {
                        reverseOrder = false;
                        currentWaypoint += 2;
                    }
                    targetPosition = waypoints[currentWaypoint].position;
                }
                break;
        }
    }
}
