using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public InputField inputField; // Reference to the input field for the speed value
    Boolean value_is_float = true; // Flag to track whether the input is a valid float
    private float input; // Parsed float value of the input
    private string line; // The text entered in the input field
    public Text errorMessage; // Reference to the error message text

    private void Start()
    {
        errorMessage.GetComponent<Text>().enabled = false; // Disable the error message text initially
        string path = "speed.txt"; // Path to the speed configuration file
        using (StreamReader reader = new StreamReader(path))
        {
            string text = reader.ReadLine();
            if (text != null)
            {
                inputField.text = text; // Set the input field text to the value from the configuration file if it's not empty
            }
        }
    }

    public void ClickOnBack()
    {
        CheckingInput(); // Check the validity of the input

        if (value_is_float)
        {
            string path = "speed.txt"; // Path to the speed configuration file
            using (StreamWriter reader = new StreamWriter(path))
            {
                reader.Write(input); // Write the validated input to the configuration file
            }
            SceneManager.LoadScene(0, LoadSceneMode.Single); // Load the main menu scene
        }
        else
        {
            errorMessage.GetComponent<Text>().enabled = true; // Show the error message if the input is invalid
        }
    }

    private void CheckingInput()
    {
        line = inputField.text; // Get the text from the input field
        try
        {
            input = float.Parse(line.Replace('.', ',')); // Try to parse the input as a float
            value_is_float = true; // Set the flag as true if parsing is successful
        }
        catch (Exception e)
        {
            value_is_float = false; // Set the flag as false if parsing fails
        }

        if (input >= 100)
        {
            value_is_float = false; // If input is greater than or equal to 100, set the flag as false
        }
    }

    public void ChangingInput()
    {
        errorMessage.GetComponent<Text>().enabled = false; // Hide the error message when input is changed
    }
}
