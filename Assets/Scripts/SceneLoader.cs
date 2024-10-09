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
        SceneManager.LoadScene("SceneSpriteMouvement", LoadSceneMode.Single);
        Debug.Log("Play");
    }
}
