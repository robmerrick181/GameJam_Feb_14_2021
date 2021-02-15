using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{

    public void VolumeSlider(float _val)
    {
        print(_val);
    }

    public void FullScreen(int _val)
    {
        if (_val == 0)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }

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
