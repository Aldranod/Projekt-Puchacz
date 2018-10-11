using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public string OwlType = "puchacz";

    void OnMouseUp()
    {
        SceneManager.LoadScene("PuchaczGame");

    }
}
