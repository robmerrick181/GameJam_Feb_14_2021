using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public void OpenURL(string _url)
    {
        Application.OpenURL(_url);
    }
    
    public void LoadScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
