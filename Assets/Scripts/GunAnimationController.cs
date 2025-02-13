using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunAnimationController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    float horizontalInput;
    float forwardInput;
    private int PowerUp = 100;
    private int totalPower = 100;
    private int damage = 10;


    [SerializeField] float turnSpeed = 20f;
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject bulletPrefab; // Bullet prefab to instantiate
    [SerializeField] float bulletSpeed = 30f;
    [SerializeField] Vector3 val;
    GameObject focalPoint;
    DamageEvent damageEvent = new DamageEvent();
    [SerializeField] ParticleSystem blood;
    [SerializeField] ParticleSystem heal;
    [SerializeField] AudioSource BulletFire;
    [SerializeField] AudioSource BulletHitMan;
    [SerializeField] Button restartButton;
    bool isDied = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("focalPoint");
        EventManagerGTA.AddEvenInvoker(this);

    }

    void Update()
    {
        if (!isDied)
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
        BulletFire.Play();
        // Convert local offset to world position
        Vector3 bulletSpawnPos = transform.TransformPoint(new Vector3(0.1184006f, 1.4751f, 0.8540001f));

        // Instantiate bullet at the correct position
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, transform.rotation);

        // Apply force to move bullet forward
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
    public void AddenemyHitEvent(UnityAction<int> listener)
    {
        damageEvent.AddListener(listener);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")){
            if (totalPower > 0)
            {
                BulletHitMan.Play();
                Destroy(other.gameObject);
                totalPower = Mathf.Max(0, totalPower - damage);
                damageEvent.Invoke(totalPower);
                blood.Play();
            }
            else{
                animator.SetBool("isDead", true);
                isDied = true;
                restartButton.gameObject.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            damageEvent.Invoke(PowerUp);
            heal.Play();
        }
    }

}