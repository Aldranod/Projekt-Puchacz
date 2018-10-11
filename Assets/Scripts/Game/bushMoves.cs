using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bushMoves : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "victim")
        {
            GetComponent<Animator>().Play("krzakRuch");
            Debug.Log("hide in krzak");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "victim")
        {
            GetComponent<Animator>().Play("Idle");
        }
    }


}
