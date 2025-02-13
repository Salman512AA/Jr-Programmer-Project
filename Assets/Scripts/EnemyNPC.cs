using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : npcController
{
    protected float detectionRange = 50f;
    [SerializeField] private float rotationSpeed = 10f; // Speed of rotation
    private Quaternion targetRotation = Quaternion.identity;
    [SerializeField] GameObject bulletPrefab; // Bullet prefab to instantiate
    [SerializeField] float bulletSpeed = 30f;
    bool canShoot = true; // Start with shooting allowed
    float resetShootTime = 1.5f; // Time to reset shooting


    // Update is called once per frame
    protected override void Update()
    {
        if (PlayerFinder.Instance.CurrentTarget != null)
        {
            Vector3 direction = (PlayerFinder.Instance.CurrentTarget.position - transform.position+Vector3.up).normalized;
            Vector3 directionForShoot= (PlayerFinder.Instance.CurrentTarget.position - transform.position).normalized;
            Debug.DrawLine(transform.position, transform.position + direction * detectionRange, Color.green);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
            {
                if (hit.transform == PlayerFinder.Instance.CurrentTarget)
                {
                    Vector3 desiredDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0f);
                    targetRotation = Quaternion.LookRotation(desiredDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                    agent.isStopped = true; // Stop movement when aiming
                    animator.SetBool("IsFiring", true);
                    if (canShoot)
                    {
                        Shoot(directionForShoot);
                        StartCoroutine(ResetShooter()); // Start cooldown after shooting
                    }
                }
                else
                {
                    ResumeMovement();
                }
            }
            else
            {
                ResumeMovement(); // If raycast doesn't hit the player, resume movement
            }
        }
        else
        {
            ResumeMovement();
        }
    }

    // Function to resume movement
    void ResumeMovement()
    {
        animator.SetBool("IsFiring", false);
        animator.SetFloat("vertical", 1);
        agent.isStopped = false;
        MoveNPC();
    }

    protected virtual void Shoot(Vector3 dir)
    {
        if (!isDead)
        {
            bulletSound.Play();
            canShoot = false; // Disable shooting

            // Convert local offset to world position
            Vector3 bulletSpawnPos = transform.TransformPoint(new Vector3(0.1184006f, 1.4751f, 0.8540001f));

            // Instantiate bullet at the correct position
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, Quaternion.LookRotation(dir));

            // Apply force to move bullet forward
            Rigidbody rb = bullet.GetComponent<Rigidbody>();


            rb.AddForce(dir.normalized * bulletSpeed, ForceMode.Impulse);
        }
    }

    // Coroutine to reset the shooting ability after a delay
    IEnumerator ResetShooter()
    {
        yield return new WaitForSeconds(resetShootTime);
        canShoot = true; // Re-enable shooting after the cooldown
    }
}

