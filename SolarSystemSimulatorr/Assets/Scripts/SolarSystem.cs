using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SolarSystem : MonoBehaviour
{
    public GameObject obj; // Prefab for creating space bodies
    SpaceBody[] spaceBodies; // Array to hold all space bodies
    public float systemsVelocity = 0.01f; // Default systems velocity

    // Start is called before the first frame update
    void Start()
    {
        string path = "speed.txt"; // Path to the speed configuration file
        using (StreamReader reader = new StreamReader(path))
        {
            string velocity = reader.ReadLine();

            if (velocity != null)
            {
                systemsVelocity = float.Parse(velocity.Replace('.', ',')) / 100; // Read and set the systems velocity
            }
        }

        CreateBodies(); // Create space bodies
        spaceBodies = FindObjectsOfType<SpaceBody>(); // Find all space bodies in the scene

        foreach (SpaceBody spaceBody in spaceBodies)
        {
            spaceBody.SetSystemsVelocity(systemsVelocity); // Set the systems velocity for each space body
        }

        Camera.main.GetComponent<CameraMovement>().SetBodies(spaceBodies); // Set space bodies in the camera controller
    }

    private void FixedUpdate()
    {
        foreach (SpaceBody body in spaceBodies)
        {
            body.FindVelocity(spaceBodies); // Calculate velocities of each space body based on interactions
        }

        foreach (SpaceBody body in spaceBodies)
        {
            body.ChangePosition(); // Update positions of each space body based on velocities
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single); // Load main menu scene on Escape key press
        }
    }

    // Create space bodies based on configuration file
    void CreateBodies()
    {
        string path = "values.txt"; // Path to the configuration file
        using (StreamReader reader = new StreamReader(path))
        {
            int quantity;
            string name;
            float mass;
            float distance;
            float initialVelocity;
            float scale;

            quantity = int.Parse(reader.ReadLine()); // Read the quantity of space bodies

            for (int i = 0; i < quantity; i++)
            {
                GameObject newObject = GameObject.Instantiate(obj, Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject; // Instantiate a new space body

                name = reader.ReadLine(); // Read the name of the space body
                newObject.name = name;

                mass = float.Parse(reader.ReadLine()); // Read the mass of the space body
                newObject.GetComponent<Rigidbody>().mass = mass;

                distance = float.Parse(reader.ReadLine()); // Read the initial distance of the space body in 10**5 km 
                newObject.transform.position = new Vector3(distance, 0, 0);

                initialVelocity = float.Parse(reader.ReadLine()); // Read the initial velocity of the space body kin km/s
                newObject.GetComponent<SpaceBody>().initialVelocity = new Vector3(0, 0, initialVelocity);

                scale = float.Parse((reader.ReadLine())); // Read the radius of the space body
                newObject.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}

