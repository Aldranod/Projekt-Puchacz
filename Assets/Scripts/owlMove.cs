using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class owlMove : MonoBehaviour {

    //ile skoków może zrobić sowa
    public int PossibleJumpsCount = 1;
    public Text PowerLevelText;

    bool addVelovity = false;
    float time;
    Vector3 jumpVelocity;
    //ile skoków sowa wykonała
    int jumpCount = 0;
    


    void Start()
    {
        PowerLevelText.enabled = false;
    }

    void Update()
    {

        if (addVelovity)
        {
            time += Time.deltaTime;
            jumpVelocity += new Vector3(-(Time.deltaTime*10), Time.deltaTime*10.0f, 0);
            //TODO: dodać jakiś dzielnik, żeby liczba procentów nie zapierdalała
            //if (time % 5 == 0.0f)
            //{
                PowerLevelText.text = (time * 100).ToString("0.00") + "%";
            //}

            //dokonaj skoku po określonym czasie, nie pozwól trzymać w nieskończoność
            if (time > 1.0f)
            {
                Jump();
            }
        }

    }

    void OnMouseDown()
    {
        PowerLevelText.enabled = true;
        if (jumpCount < PossibleJumpsCount)
        {
            time = 0.0f;
            Debug.Log("owl click");
            addVelovity = true;
        }
    }


    void OnMouseUp()
    {
        Jump();
    }


    void Jump()
    {
        if (jumpCount < PossibleJumpsCount)
        {
            Debug.Log("owl jump");
            GetComponent<Rigidbody2D>().velocity = jumpVelocity;
            addVelovity = false;
            jumpCount++;
            PowerLevelText.enabled = false;
        }
    }


}
