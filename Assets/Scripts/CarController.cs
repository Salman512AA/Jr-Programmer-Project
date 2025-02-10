using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    [SerializeField] float horizontalInput;
    [SerializeField] float forwardInput;
    [SerializeField] float speed=10;
    [SerializeField] float turnSpeed = 45.0f;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Method to update movement input, called from another script (e.g., player input)
    

    // Called at fixed time intervals for physics-based movement
    private void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        rb.AddRelativeForce(Vector3.forward * forwardInput * speed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (transform.rotation.eulerAngles.z > 1 || transform.rotation.eulerAngles.z < -1)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
    }

}
