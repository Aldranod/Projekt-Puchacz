﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class owlMove : MonoBehaviour {

    //ile skoków może zrobić sowa
    public int PossibleJumpsCount = 1;
    public Text PowerLevelText;
    //linia która pokazuje jak będzie wystrzeliwana sowa
    public GameObject JumpLine;
    public Transform startPoint;

    bool addVelovity = false;
    float time;
    //ile skoków sowa wykonała
    int jumpCount = 0;


    #region eventy
    void Start()
    {
        PowerLevelText.enabled = false;
        JumpLine.GetComponent<LineRenderer>().SetPosition(0, transform.position);
    }

    void FixedUpdate()
    {
        CheckJumping();

    }

    void OnMouseDown()
    {
        if (jumpCount < PossibleJumpsCount)
        {
            PowerLevelText.enabled = true;
            JumpLine.SetActive(true);
            time = 0.0f;
            addVelovity = true;

        }
    }

    void OnMouseUp()
    {
        Jump();
    }

    Vector3 GetJumpVector()
    {
        Vector3 finalVector = new Vector3();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        finalVector.x = transform.position.x - mousePos.x;
        finalVector.y = transform.position.y - mousePos.y;

        return finalVector;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag == "floor")
        {
            Debug.Log("floorhit");
        }
        else if (col.gameObject.tag == "victim")
        {
            Debug.Log("kill!");
        }
    }
    #endregion

    #region obsługa kolizji

    /// <summary>
    /// po uderzeniu odlicza chwile, zanim zrestartuje pozycje sowy
    /// </summary>
    void StartCountToReset()
    {

    }

    //resetuje pozycje sowy
    void Reset()
    {
        jumpCount = 0;
        transform.position = startPoint.position;
    }
    #endregion


    #region obsługa skoku
    /// <summary>
    /// Sprawdzanie, czy sytępuje skok i jego obsługa
    /// </summary>
    void CheckJumping()
    {
        if (addVelovity)
        {

            UpdateLine(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            time += Time.deltaTime;
            // jumpVelocity += new Vector3(Time.deltaTime, Time.deltaTime, 0);
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

    void Jump()
    {
        if (jumpCount < PossibleJumpsCount)
        {
            GetComponent<Rigidbody2D>().velocity = GetJumpVector();
            addVelovity = false;
            jumpCount++;
            PowerLevelText.enabled = false;
            DestroyLine();
        }
    }

    void UpdateLine(Vector3 end)
    {
        LineRenderer lr = JumpLine.GetComponent<LineRenderer>();
        lr.SetPosition(1, new Vector3(end.x, end.y, transform.position.z));
    }

    void DestroyLine()
    {
        JumpLine.SetActive(false);
    }

    #endregion
}
