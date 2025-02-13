using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : ParentTank
{
    [SerializeField] float horizontalInput;
    [SerializeField] float forwardInput;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed = 45.0f;
    private Rigidbody rb;
    //[SerializeField] GameObject bulletPrefab; // Bullet prefab to instantiate
    //[SerializeField]Vector3 pos=new Vector3(-5,3.5f,0);
    //[SerializeField] ParticleSystem smokeParticle;
    //[SerializeField] ParticleSystem fireParticle;
    //[SerializeField] AudioSource soundEffect;]
    public int CurrentPower
    {
        get { return totalPower; }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameEnd)
        {
            forwardInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            rb.AddRelativeForce(Vector3.forward * forwardInput * speed);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        }
        //stable
        if (transform.rotation.eulerAngles.z >1|| transform.rotation.eulerAngles.z <-1)
        {
            transform.rotation =Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Fire on pressing Spacebar
        {
            Vector3 fireDir = transform.forward;
            if (bulletEnabled)
            {
                FireBullet(fireDir);
                StartCoroutine(BulletFireAllowed());
            }
        }
       
    }
    protected override void FireBullet(Vector3 bulletDirection)
    {
        if (!gameEnd)
        {
            bulletEnabled = false;

            smokeParticle.Play();
            fireParticle.Play();
            soundEffect.Play();
            Vector3 bulletSpawnPosition = transform.position + transform.forward * 4f + Vector3.up*1.5f; // Adjust the distance as needed
            Debug.Log(bulletSpawnPosition);

            // Instantiate bullet with the same rotation as the tank's rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, transform.rotation);
            bullet.tag = "Bullet";


            // Add force in the forward direction of the tank (taking into account its current rotation)
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(bulletDirection * attackForce, ForceMode.Impulse); // Fire in tank's forward direction
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(5);
            Destroy(collision.gameObject);
        }
    }
    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        playerHitEvent.Invoke(totalPower);
       

    }

}
