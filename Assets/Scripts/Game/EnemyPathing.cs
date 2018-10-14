using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    public List<Transform> waypoints;
    public float moveSpeed = 2f;
    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //if transform.position.x < previous.transform.position.x
        //{
        //    mousego left
        //}
        //else
        //{
        //    mousego right
        //}
        Move();
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
