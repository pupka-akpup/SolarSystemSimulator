using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * the numerology of scenes:
 * 0 - menu
 * 1 - settings scene
 * 2 - main scene
 */
public class Menu : MonoBehaviour
{   
    //function that loads main scene if user clicks button "start"
    public void ClickOnStart()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    //function that loads settings scene if user clicks button "settings"
    public void ClickOnSettings()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    //function that closes the program if user clicks button "quit"
    public void ClickOnQuit()
    {
        Application.Quit();
    }
}
