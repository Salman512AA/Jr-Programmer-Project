using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    [SerializeField] float horizontalInput;
    [SerializeField] float forwardInput;
    [SerializeField] float speed=10;
    [SerializeField] float turnSpeed = 45.0f;
    [SerializeField]  AudioSource CarSound;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //GameObject soundObject = GameObject.Find("CarSound");
        //if (soundObject != null)
        //{
        //        CarSound = soundObject.GetComponent<AudioSource>();
        //}
    }
    // Method to update movement input, called from another script (e.g., player input)
    

    // Called at fixed time intervals for physics-based movement
    private void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (forwardInput != 0|| horizontalInput!=0)
        {
            if (!CarSound.isPlaying)
            {
                CarSound.Play();
            }
            
        }
        else
        {
            CarSound.Stop();
        }
        rb.AddRelativeForce(Vector3.forward * forwardInput * speed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (transform.rotation.eulerAngles.z > 1 || transform.rotation.eulerAngles.z < -1)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
    }
    public void OnDisable()
    {
        if (CarSound != null && CarSound.isPlaying)
        {
            CarSound.Stop();
        }
    }

}
