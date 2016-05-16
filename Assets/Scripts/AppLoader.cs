using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AppLoader : MonoBehaviour {

    public void LoadNewGame()
    {
        SceneManager.LoadScene("Main Map Scene");
    }
}
