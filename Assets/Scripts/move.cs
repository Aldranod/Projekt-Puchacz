using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    [SerializeField] float speed = 0.5f;
    [SerializeField] GameObject spawningPoint;
    [SerializeField] GameObject spawningPoint2;
    bool mouseTurn = true;
    
    // Use this for initialization
    void Start ()
    {
 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(mouseTurn)
        {
            MouseGoLeft();
            if(transform.position.x < spawningPoint.transform.position.x)
            {
                mouseTurn = false;
            }
        }
        else
        {
            MouseGoRight();
            if(transform.position.x > spawningPoint2.transform.position.x)
            {
                mouseTurn = true;
            }
        }
    }

    void MouseGoLeft()
    {
        Vector2 mousePos = new Vector2(transform.position.x, transform.position.y);
        mousePos.x = mousePos.x - speed * Time.deltaTime;
        transform.position = mousePos;
    }

    void MouseGoRight()
    {
        Vector2 mousePos = new Vector2(transform.position.x, transform.position.y);
        mousePos.x = mousePos.x + speed * Time.deltaTime;
        transform.position = mousePos;
    }
}
