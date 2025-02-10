using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraScript : MonoBehaviour
{
    public Rigidbody rb;               // Reference to the Rigidbody
    public float speed = 10f;           // Forward/backward speed
    public float turnSpeed = 150f;      // Steering speed

    private float moveInput;            // Store the vertical input (W/S or Up/Down arrows)
    private float turnInput;            // Store the horizontal input (A/D or Left/Right arrows)

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        // Get the player input
        moveInput = Input.GetAxis("Vertical"); // Forward/Backward (W/S or Up/Down)
        turnInput = Input.GetAxis("Horizontal"); // Left/Right (A/D or Left/Right)
    }

    void FixedUpdate()
    {
        // Move the car forward or backward
        Vector3 move = transform.forward * moveInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move); // Apply movement to the Rigidbody

        // Turn the car left or right
        float turn = turnInput * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f); // Create a rotation around the Y axis
        rb.MoveRotation(rb.rotation * turnRotation); // Apply the rotation
    }
}
