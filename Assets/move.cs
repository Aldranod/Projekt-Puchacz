using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    [SerializeField] float speed = 0.5f;
    
    //cashed reference
    GameObject spawningPoint;
    
    // Use this for initialization
    void Start () {
        spawningPoint = GameObject.FindWithTag("spawningPoint");
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = new Vector2(transform.position.x, transform.position.y);
        
        if (mousePos.x > spawningPoint.transform.position.x)
        {
            mousePos.x = mousePos.x - speed * Time.deltaTime;
            transform.position = mousePos;
        }
        else
        {
            mousePos.x = mousePos.x + speed * Time.deltaTime;
            transform.position = mousePos;
        }
 
    }
}
