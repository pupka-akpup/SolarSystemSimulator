using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class SpaceBody : MonoBehaviour
{
    public Vector3 initialVelocity; // Initial velocity of the space body

    Vector3 velocity; // Current velocity of the space body
    readonly float G = 3.98584628f; // Gravitational constant

    float systemsVelocity; // Systems velocity for time scaling

    void Start()
    {
        velocity = initialVelocity; // Initialize velocity with the provided initial velocity
    }

    // Calculate the velocity of the space body based on gravitational interactions with other bodies
    public void FindVelocity(SpaceBody[] spaceBodies)
    {
        foreach (SpaceBody body in spaceBodies)
        {
            if (!body.Equals(this)) // Avoid self-interaction
            {
                float m1 = body.GetComponent<Rigidbody>().mass; // Mass of the other body

                float r = (body.transform.position - this.transform.position).sqrMagnitude; // Distance squared
                Vector3 direction = (-this.transform.position + body.transform.position).normalized; // Direction vector

                Vector3 acceleration = G * direction * m1 / r; // Gravitational acceleration

                velocity += acceleration * systemsVelocity; // Update velocity

                // Stop the body if it gets too close to another body
                if ((this.GetComponent<Transform>().position - body.GetComponent<Transform>().position).magnitude < 10)
                {
                    velocity = Vector3.zero; // Set velocity to zero
                }
            }
        }
    }

    // Set the systems velocity for time scaling
    public void SetSystemsVelocity(float systemsVelocity)
    {
        this.systemsVelocity = systemsVelocity;
    }

    // Update the position of the space body based on its velocity
    public void ChangePosition()
    {
        this.GetComponent<Rigidbody>().position += velocity * systemsVelocity;
    }

    // Get the magnitude of the velocity
    public float GetVelocity()
    {
        return velocity.magnitude;
    }

    // Respond to clicking on the space body by initiating follow mode in the camera
    void OnMouseDown()
    {
        CameraMovement camera = Camera.main.GetComponent<CameraMovement>();
        camera.StartFollow(this, true); // Start following this space body
    }
}

