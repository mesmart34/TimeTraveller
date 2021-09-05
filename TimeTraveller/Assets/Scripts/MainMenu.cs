using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        var currentLevel = PlayerPrefs.GetInt("Level");
        if(currentLevel == 0 || currentLevel == 5)
            currentLevel = 1;
        else 
            currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}
