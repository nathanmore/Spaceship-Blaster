using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("main");
    }
    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Additive);
    }

    public void CloseOptionsMenu()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }

    public void OpenControls()
    {
        SceneManager.LoadScene("ControlsMenu", LoadSceneMode.Additive);
    }

    public void CloseControlsMenu()
    {
        SceneManager.UnloadSceneAsync("ControlsMenu");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            OptionSettings.SystemMute();
            SceneManager.LoadScene("WebGLQuit");
        }
        else
        {
            Application.Quit();
        }
    }
}
