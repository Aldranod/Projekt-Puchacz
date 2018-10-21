using UnityEngine;

public class owlFly : MonoBehaviour, IResetable, IMove {

    public float speed = 0.1f;
    owlManager manager;
    
    void Start()
    {
        manager = GetComponent<owlManager>();
        manager.defaultAnimation = "fly";
        GetComponent<Animator>().Play("fly");
        manager.allowPowerDive = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!manager.isControlDisabled)
        {
            if(col.gameObject.tag == "turnPoint")
            { 
                speed = -speed;
            }
        }
    }
    public void CheckMove()
    {
        if (!manager.isControlDisabled)
        {
            gameObject.transform.Translate(speed, 0.0f, 0.0f);
        }
    }

    public void Reset()
    {
        manager.allowPowerDive = true;
    }
}
