using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class owlJump : MonoBehaviour, IResetable, IMove {

    //ile skoków może zrobić sowa
    public int PossibleJumpsCount = 1;
    public bool UseTimer = false;
    public Text PowerLevelText;
    //linia która pokazuje jak będzie wystrzeliwana sowa
    public GameObject JumpLine;

    owlManager manager;
    //ile skoków sowa wykonała
    int jumpCount = 0;
    float time;
    bool addVelovity = false;
    //trzeba poczekac z pozwoleniem na dive, aby oba mouseUpy sie nie nakładały
    bool isWaitingToAllowDive = false;


    // Use this for initialization
    void Start () {
        manager = GetComponent<owlManager>();
        manager.allowPowerDive = false;
        PowerLevelText.enabled = false;
        JumpLine.GetComponent<LineRenderer>().SetPosition(0, transform.position);
    }

    void FixedUpdate()
    {
        if (isWaitingToAllowDive)
        {
            CountToDive();
        }
    }
    /// <summary>
    ///  odlicza chwile, zanim zrestartuje pozycje sowy
    /// </summary>
    void CountToDive()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            manager.allowPowerDive= true;
            isWaitingToAllowDive = false;
            time = 0f;
        }

    }

    void OnMouseDown()
    {
        if (jumpCount < PossibleJumpsCount)
        {
            manager.isJumppingMode = true;
            if (UseTimer)
            {
                PowerLevelText.enabled = true;
                time = 0.0f;
            }
            ReInitializeLine();
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

    #region obsługa skoku
    /// <summary>
    /// Sprawdzanie, czy sytępuje skok i jego obsługa
    /// </summary>
    void CheckJumping()
    {
        if (addVelovity)
        {

            UpdateLine(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (UseTimer)
            {
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

    }

    void Jump()
    {
        if (jumpCount < PossibleJumpsCount)
        {
            GetComponent<Animator>().Play("fly");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().velocity = GetJumpVector();
            addVelovity = false;
            jumpCount++;
            PowerLevelText.enabled = false;
            DestroyLine();
            manager.isControlDisabled = true;
            isWaitingToAllowDive = true;
            manager.isJumppingMode = false;
        }
    }

    //dodane, zeby wyzerowac linie zanim się aktyruje jej widok, bo inaczej brzydko miga
    void ReInitializeLine()
    {
        JumpLine.SetActive(true);
        JumpLine.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        JumpLine.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
    }

    void UpdateLine(Vector3 end)
    {
        LineRenderer lr = JumpLine.GetComponent<LineRenderer>();
        lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        lr.SetPosition(1, new Vector3(end.x, end.y, transform.position.z));
    }

    void DestroyLine()
    {
        JumpLine.SetActive(false);
    }

    #endregion

    #region implementacja interfejsów
    public void Reset()
    {
        manager.allowPowerDive = false;
        jumpCount = 0;
        addVelovity = false;
    }
    public void CheckMove()
    {
        CheckJumping();
    }
    #endregion
}
