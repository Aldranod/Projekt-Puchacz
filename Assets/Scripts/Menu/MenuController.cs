using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public OwlType owlType;

    void OnMouseUp()
    {
        switch (owlType)
        {
            case OwlType.płomykówka:
                SceneManager.LoadScene("Latanie");
                break;
            case OwlType.puchacz:
                SceneManager.LoadScene("Skakanie");
                break;
            default:
                break;
        }

    }
}
