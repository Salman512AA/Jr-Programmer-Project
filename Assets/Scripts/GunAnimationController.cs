using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    float horizontalInput;
    float forwardInput;

    [SerializeField] float turnSpeed = 20f;
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject bulletPrefab; // Bullet prefab to instantiate
    [SerializeField] float bulletSpeed = 30f;
    [SerializeField] Vector3 val;
    GameObject focalPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("focalPoint");
        
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");



        animator.SetFloat("Vertical", forwardInput);

        // Handle firing animation
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsFiring", true);
            Shoot();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsFiring", false);
        }
    }

    void FixedUpdate()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found!");
            return;
        }
       

        //// Move Forward
        transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);

        //// Rotate
       transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
    void Shoot()
    {
        // Convert local offset to world position
        Vector3 bulletSpawnPos = transform.TransformPoint(new Vector3(0.1184006f, 1.4751f, 0.8540001f));

        // Instantiate bullet at the correct position
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, transform.rotation);

        // Apply force to move bullet forward
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

}

