using UnityEngine;
using System.Collections;

public class ApplicationScript : MonoBehaviour {


    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
