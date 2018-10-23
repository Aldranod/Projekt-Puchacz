using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCameraAfterOwl : MonoBehaviour, IResetable {

    Vector2 owlStartingPoint;
    Camera mainCamera;
    //różnica w położeniu na osi Y między kamerą, a sową
    float yDifference = 0f;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        owlStartingPoint = transform.position;
        yDifference = mainCamera.transform.position.y - transform.position.y ;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (transform.position.y > owlStartingPoint.y)
        {
          mainCamera.transform.position =   new Vector3(mainCamera.transform.position.x, transform.position.y+ yDifference, mainCamera.transform.position.z); 
        }
    }

    public void Reset()
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, owlStartingPoint.y + yDifference, mainCamera.transform.position.z);
    }
}
