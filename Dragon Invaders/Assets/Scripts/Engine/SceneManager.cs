using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void OpenConfigScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Configuration", LoadSceneMode.Single);
    }
}
