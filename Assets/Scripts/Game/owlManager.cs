using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class owlManager : MonoBehaviour {

    public RigidbodyType2D startingBodytype = RigidbodyType2D.Kinematic;
    //czy użytkownik naciąga sowe do skoku
    public bool isJumppingMode = false;
    //czu użytkownik wykonał skok
    public bool isControlDisabled = false;
    //czy w danej chwili mozna uzyc powerDive
    public bool allowPowerDive = false;
    public string defaultAnimation = "Idle";

    bool isWatingForReset = false;
    float time = 0f;
    IResetable[] scripts;

	// Use this for initialization
	void Start () {
        scripts = GetComponents<IResetable>();
        GetComponent<Rigidbody2D>().bodyType = startingBodytype;

    }

    void FixedUpdate()
    {
        if (isWatingForReset)
        {
            CountToReset();
        }
    }
    /// <summary>
    ///  odlicza chwile, zanim zrestartuje pozycje sowy
    /// </summary>
    void CountToReset()
    {
        time -= Time.deltaTime;
        if (time < 0f)
        {
            _reset();
        }

    }

    public void OwlReset(float timeToReset)
    {
        time = timeToReset;
        isWatingForReset = true;
    }

    void _reset()
    {
        foreach (IResetable script in scripts)
        {
            script.Reset();
        }
        isWatingForReset = false;
        GetComponent<Rigidbody2D>().bodyType =  startingBodytype;
        isControlDisabled = false;
        GetComponent<Animator>().Play(defaultAnimation);
    }
}
