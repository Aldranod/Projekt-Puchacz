using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class owlMove : MonoBehaviour, IResetable
{
    public Transform StartPoint;
    public bool UsePowerDive = true;
    public GameObject feathersParticle;
    public float divePower = 7f;
    public int possibleDiveCount = 1;

    IMove moveScript;
    owlManager manager;
    float time;


    #region eventy
    void Start()
    {
        manager = GetComponent<owlManager>();
        moveScript = GetComponent<IMove>();
    }

    void FixedUpdate()
    {
        moveScript.CheckMove();
        CheckDive();
    }
       
    void OnTriggerEnter2D(Collider2D col)
    {
        ReactToCollisions(col.tag);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        ReactToCollisions(col.gameObject.tag);
    }

    void ReactToCollisions(string colliderTag)
    {
        switch (colliderTag)
        {
            case "hideout":
                StandardCollisionStop(1f);
                break;
            case "floor":
                StandardCollisionStop(2f);
                break;
            case "victim":

                // GameObject.Destroy(col.gameObject);
                StandardCollisionStop(2f);
                break;
            case "owlCatch":
                CallReset(1f);
                break;
            default:
                break;
        }
    }
    //standardowe zachowanie w przypadku kolizji - zatrzymaj sowe, pusc animacje idle, reset gry
    void StandardCollisionStop(float timeToReset)
    {
        manager.allowPowerDive = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Animator>().Play("Idle");
        CallReset(timeToReset);
    }

    #endregion

    #region power dive
    int diveCount = 0;
    bool isDive = false;
    void CheckDive()
    {
        if(UsePowerDive && manager.allowPowerDive && (diveCount<possibleDiveCount))
        {
            //oczekuj na przycisk myszy
            if (Input.GetMouseButtonUp(0))
            {
                isDive = true;
              
            }
            else if (isDive)
            {
                Dive();
                diveCount++;
            }
        }
    }

    void Dive()
    {
        isDive = false;
        manager.isControlDisabled = true;
        feathersParticle.transform.position = transform.position;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        feathersParticle.GetComponent<ParticleSystem>().Play();
        GetComponent<Animator>().Play("pikowanie");
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
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    //resetuje pozycje sowy
    public void Reset()
    {
        transform.position = StartPoint.position;
        StopMove();
        diveCount = 0;
    }
    #endregion
    
  
}
