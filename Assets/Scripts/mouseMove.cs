using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMove : MonoBehaviour {

    [SerializeField] float directionChangeTime = 2f;
    private float lastestDirectionChangeTime;
    [SerializeField] float characterVelocity = 50f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    // Use this for initialization
    void Start ()
    {
        lastestDirectionChangeTime = 0f;
        RandomMovement();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Time.time - lastestDirectionChangeTime > directionChangeTime)
        {
            lastestDirectionChangeTime = Time.time;
            RandomMovement();
        }
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y);
    }

    void RandomMovement()
    {
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), transform.position.y).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }
}
