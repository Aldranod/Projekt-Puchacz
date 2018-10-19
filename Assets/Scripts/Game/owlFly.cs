using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class owlFly : MonoBehaviour, IResetable {

    public float speed = 0.1f;
    owlManager manager;
    
    void Start()
    {
        manager = GetComponent<owlManager>();
        GetComponent<Animator>().Play("fly");
    }

    void FixedUpdate()
    {
        if (!manager.isAfterJump)
        {
            gameObject.transform.Translate(speed, 0.0f, 0.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!manager.isAfterJump)
        {
            if(col.gameObject.tag == "turnPoint")
            { 
                speed = -speed;
            }
        }
    }

    public void Reset()
    {
        GetComponent<Animator>().Play("fly");
    }
}
