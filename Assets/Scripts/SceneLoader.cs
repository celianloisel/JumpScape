using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("BuilderScene", LoadSceneMode.Single);
        Debug.Log("Play");
    }

    public void Home()
    {
        SceneManager.LoadScene("StartingMenu", LoadSceneMode.Single);
        Debug.Log("Home");
    }
    
    public void LaucnhGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        Debug.Log("Launch");
    }
}
