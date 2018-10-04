using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    //cashed reference
    GameObject spawningPoint;

	// Use this for initialization
	void Start () {
        spawningPoint = GameObject.FindWithTag("spawningPoint");
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 mousePos = new Vector2(transform.position.x, transform.position.y);
        mousePos.x = spawningPoint.transform.position.x - 1;
        transform.position = mousePos;
    }
}
