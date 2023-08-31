using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    // Reference to the main camera
    Camera camera;

    // Reference to the sun and canvas
    public SpaceBody sun;
    public Canvas canvas;

    // Movement vectors
    Vector3 left;
    Vector3 right;
    Vector3 up;
    Vector3 down;

    // Flag for following a space body
    Boolean follow;

    // Currently followed space body and an array of all space bodies
    SpaceBody body;
    SpaceBody[] spaceBodies;

    // Counter for space body array traversal
    int counter;

    // Start is called before the first frame update
    void Start()
    {
        // Get the main camera
        camera = Camera.main;

        // Initialize following to false
        follow = false;

        // Define movement vectors
        left = new Vector3(-5, 0, 0);
        right = new Vector3(5, 0, 0);
        up = new Vector3(0, 0, 5);
        down = new Vector3(0, 0, -5);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the camera is enabled
        if (camera.enabled)
        {
            ChangingSize(); // Adjust the camera's orthographic size

            if (!follow) // If user is not currently following any space body 
            {
                MovingCamera(); // Move the camera with input

                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartFollow(spaceBodies[counter], false); // Follow the selected space body
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                CounterChange(); // Move to the next space body
                if (spaceBodies[counter].name == "Body")
                {
                    CounterChange(); // Skip the body named "Body"
                }
                StartFollow(spaceBodies[counter], false); // Start following space body according to counter
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                follow = false; // Stop following
                counter = spaceBodies.Length - 2; // Reset counter
                canvas.GetComponent<CanvasChanges>().HideTexts(); // Hide UI texts
            }
            else
            {
                Following(this.body); // Continue following the current space body
            }
        }
    }

    // Start following a space body
    public void StartFollow(SpaceBody body, Boolean clickFollow)
    {
        follow = true;
        this.body = body;

        // Show UI texts and information about the body
        canvas.GetComponent<CanvasChanges>().ShowTexts();
        canvas.GetComponent<CanvasChanges>().AddInfo(body.name, body.GetComponent<Rigidbody>().mass, (body.transform.position - sun.transform.position).magnitude, body.GetComponent<SpaceBody>().GetVelocity());

        if (clickFollow) counter = FindIndex(); // If user clicked on the body, find index of it, so the changing of the bodies using space can work correctly.
    }

    // Follow a specific space body
    void Following(SpaceBody body)
    {
        // Set the camera position above the space body and update UI info
        camera.GetComponent<Transform>().position = new Vector3(body.transform.position.x, 1000f, body.transform.position.z);
        canvas.GetComponent<CanvasChanges>().AddInfo(body.name, body.GetComponent<Rigidbody>().mass, (body.transform.position - sun.transform.position).magnitude / 10, body.GetComponent<SpaceBody>().GetVelocity());
    }

    // Move the camera based on input
    void MovingCamera()
    {
        if (Input.GetKey(KeyCode.W))
        {
            camera.GetComponent<Transform>().position += up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            camera.GetComponent<Transform>().position += down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            camera.GetComponent<Transform>().position += left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            camera.GetComponent<Transform>().position += right;
        }
    }

    // Adjust the camera's orthographic size based on input
    void ChangingSize()
    {
        if ((Input.GetKey(KeyCode.UpArrow)) && (camera.GetComponent<Camera>().orthographicSize < 35000))
        {
            camera.GetComponent<Camera>().orthographicSize += 10;
        }
        if ((Input.GetKey(KeyCode.DownArrow)) && (camera.GetComponent<Camera>().orthographicSize > 10))
        {
            camera.GetComponent<Camera>().orthographicSize -= 10;
        }
    }

    // Decrease the counter for switching space bodies
    void CounterChange()
    {
        counter -= 1;
        if (counter == -1)
        {
            counter = spaceBodies.Length - 1;
        }
    }

    // Set the array of space bodies and initialize counter
    /*
     * Array of spaceBodies have all the bodies in order from furthest to the sun to closest
     * so counter start from the end of array
     */
    public void SetBodies(SpaceBody[] spaceBodies)
    {
        this.spaceBodies = spaceBodies;
        counter = spaceBodies.Length - 2;
    }

    // Find the index of the currently followed space body
    int FindIndex()
    {
        for (int i = 0; i < spaceBodies.Length; i++)
        {
            if (spaceBodies[i].name == body.name) return i;
        }

        return 0; // Default to the first body if not found
    }
}
