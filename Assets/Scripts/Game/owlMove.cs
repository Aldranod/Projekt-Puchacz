using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class owlMove : MonoBehaviour, IResetable
{

    //ile skoków może zrobić sowa
    public int PossibleJumpsCount = 1;
    public Text PowerLevelText;
    //linia która pokazuje jak będzie wystrzeliwana sowa
    public GameObject JumpLine;
    public Transform StartPoint;
    public bool UseTimer = false;
    public bool UsePowerDive = true;

    owlManager manager;
    bool addVelovity = false;
    float time;
    //ile skoków sowa wykonała
    int jumpCount = 0;


    #region eventy
    void Start()
    {
        manager = GetComponent<owlManager>();
        PowerLevelText.enabled = false;
        JumpLine.GetComponent<LineRenderer>().SetPosition(0, transform.position);
    }

    void FixedUpdate()
    {
        CheckJumping();
        CheckDive();
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "hideout")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Animator>().Play("Idle");
            CallReset(1f);
        }
    }
        void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            GetComponent<Animator>().Play("Idle");
            CallReset(2f);
        }
        else if (col.gameObject.tag == "victim")
        {
            GameObject.Destroy(col.gameObject);
            CallReset(2f);
            GetComponent<Animator>().Play("Idle");
        }

        else if (col.gameObject.tag == "owlCatch")
        {
            CallReset(1f);
        }
    }
    #endregion

    #region power dive

    float divePower = 5f;
    bool isDive = false;
    void CheckDive()
    {
        if(UsePowerDive && manager.isAfterJump)
        {
            //oczekuj na przycisk myszy
            if (Input.GetMouseButtonUp(0))
            {
                isDive = true;
                Debug.Log("DIVE");
            }
            else if (isDive)
            {
                Dive();
            }
        }
    }

    void Dive()
    {
        GetComponent<Rigidbody2D>().AddForce(-(transform.up) * divePower, ForceMode2D.Impulse);
    }
    #endregion

    #region obsługa restartu

    void CallReset(float time)
    {
        manager.OwlReset(time);
    }

    void StopMove()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        addVelovity = false;
        isDive = false;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    //resetuje pozycje sowy
    public void Reset()
    {
        jumpCount = 0;
        transform.position = StartPoint.position;
        StopMove();

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
            manager.isAfterJump = true;
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
}
