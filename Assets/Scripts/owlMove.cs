using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class owlMove : MonoBehaviour {


    bool moving = false;
    float t;
    void Start()
    {

    }

    void Update()
    {

        if (moving)
        {
            // when the cube has moved over 1 second report it's position
            //t = t + Time.deltaTime;
            //if (t > 1.0f)
            //{
            //    Debug.Log(gameObject.transform.position.y + " : " + t);
            //    t = 0.0f;
            //    moving = false;
            //}
        }

    }

    void OnMouseDown()
    {
        Debug.Log("owl click");
        Move();
    }

    void Move()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(-2, 10, 0);
        moving = true;

    }


}
