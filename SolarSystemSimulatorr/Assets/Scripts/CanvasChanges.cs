using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasChanges : MonoBehaviour
{
    // Public Text variables for UI elements
    public Text focus;
    public Text unfocus;
    public Text info;
    public Text space;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize texts' visibility
        focus.enabled = true;
        unfocus.enabled = false;

        space.enabled = false;
        info.enabled = false;
    }

    // Show texts for user while they are in focused mode
    public void ShowTexts()
    {
        focus.enabled = false;
        unfocus.enabled = true;
        info.enabled = true;
        space.enabled = true;
    }

    // Opposite to ShowMessages() 
    public void HideTexts()
    {
        focus.enabled = true;
        unfocus.enabled = false;
        info.enabled = false;
        space.enabled = false;
    }

    // Add information about space objects: name, their mass in earth masses, distance from the sun and velocity
    public void AddInfo(string name, float earthMasses, float distance, float velocity)
    {
        info.enabled = true;
        info.text = name + '\n' + "mass: " + earthMasses + " earth masses" + '\n' + "distance to sun: " + distance + " million km" + '\n' + "velocity: " + velocity + " km/s";
    }

    // Quit the application when clicking on button in right upper corner
    public void ClickOnQuit()
    {
        Application.Quit();
    }
}

