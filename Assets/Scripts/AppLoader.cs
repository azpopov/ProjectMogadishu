using UnityEngine;
using System.Collections;

public class AppLoader : GameElement {

    public void LoadNewGame()
    {
        Application.LoadLevel("Main Map Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
