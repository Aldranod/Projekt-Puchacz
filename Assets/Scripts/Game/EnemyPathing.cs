using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    public List<Transform> waypoints;
    public float moveSpeed = 2f;
    int waypointIndex = 0;
    float previousXPos;

	// Use this for initialization
	void Start () {
        transform.position = waypoints[waypointIndex].transform.position;
        previousXPos = transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        previousXPos = transform.position.x;
        Move();
        MouseRotate();
    }

    private void MouseRotate()
    {
        if (transform.position.x > previousXPos)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Move()
    {
        if(waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}
