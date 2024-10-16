using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
	public int level;

	public void Start() {

	    if (SceneManager.GetActiveScene().name == "BuilderScene")
        {
			level = GameData.level;
            if (level == 2)
        	{
            	GameObject endingPlatform = GameObject.FindWithTag("EndingPlateform");
            	if (endingPlatform != null)
            	{
                	endingPlatform.transform.position = new Vector3(
                    	endingPlatform.transform.position.x,
                    	endingPlatform.transform.position.y + 12.6f,
                    	endingPlatform.transform.position.z
                	);

            	}
        	}	
        }
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame(int level)
    {
		GameData.level = level;

        SceneManager.LoadScene("BuilderScene", LoadSceneMode.Single);
    }

    public void Home()
    {
        SceneManager.LoadScene("StartingMenu", LoadSceneMode.Single);
    }
    
    public void LaunchGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

	public void LevelSelect()
    {
        SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }
}
