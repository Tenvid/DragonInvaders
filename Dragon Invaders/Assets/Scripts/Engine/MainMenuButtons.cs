using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void OpenConfigScene()
    {
        SceneManager.LoadScene("Configuration", LoadSceneMode.Single);
    }
}
