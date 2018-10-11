using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {

    bool isDebugMode = false;
	
	// Update is called once per frame
	void Update () {

        if (!isDebugMode &&Input.GetKeyDown(KeyCode.D))
        {
            StartDebugMode();
        }

    }

    void StartDebugMode()
    {
        GameObject owl = GameObject.FindGameObjectWithTag("owl");
        owl.GetComponent<Animator>().Play("roland");
        owl.GetComponent<SpriteRenderer>().color = Color.white;
        GameObject victim = GameObject.FindGameObjectWithTag("victim");
        if (victim)
        {
            victim.GetComponent<Animator>().Play("crisps");
            victim.GetComponent<Transform>().localScale = new Vector2(2, 2);
            victim.GetComponent<SpriteRenderer>().color = Color.white;
        }
        isDebugMode = true;
    }
}
