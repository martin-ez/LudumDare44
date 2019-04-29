using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public SceneChoice scene;

    // Start is called before the first frame update
    public void NextScene()
    {
        SceneManager.LoadScene((int)scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public enum SceneChoice
{
    MainMenu,
    PickDragon,
    Level1
}