using UnityEngine;
using System.Collections;

public class AppLoader : MonoBehaviour {

    public void LoadNewGame()
    {
        Application.LoadLevel("Main Map Scene");
    }
}
