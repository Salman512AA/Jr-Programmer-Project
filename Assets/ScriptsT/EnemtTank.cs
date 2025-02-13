using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : ParentTank
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform playerTank;
    [SerializeField] private float rotationSpeed = 10f; // Speed of rotation
    private int currentWaypointIndex;
    private Quaternion targetRotation = Quaternion.identity;
    private Rigidbody tankRigidbody;
    private TankMovement playerTankMove;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Cache components and choose initial waypoint
        tankRigidbody = GetComponent<Rigidbody>();
        playerTankMove = playerTank.GetComponent<TankMovement>();
        ChooseWaypoint();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTankMove == null || agent == null) return;

        Vector3 direction = playerTank.position - transform.position + Vector3.up;

        // Check if the player tank is active and has power
        if (playerTankMove.CurrentPower > 0)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            Debug.DrawRay(transform.position, direction.normalized * 100f, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform == playerTank)
                {
                    // Rotate toward the player
                    Vector3 desiredDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0f);
                    targetRotation = Quaternion.LookRotation(desiredDirection);
                    tankRigidbody.MoveRotation(targetRotation);

                    // Stop the agent and fire if possible
                    agent.isStopped = true;
                    if (bulletEnabled)
                    {
                        FireBullet(direction);
                        StartCoroutine(BulletFireAllowed());
                    }
                }
                else
                {
                    ResumeAgentMovement();
                }
            }
        }

        // Check if the agent has reached its waypoint
        if (!agent.isStopped && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            ChooseWaypoint();
        }
    }

    protected override void FireBullet(Vector3 bulletDirection)
    {
        if (gameEnd) return;

        bulletEnabled = false;

        // Play visual and audio effects
        smokeParticle.Play();
        fireParticle.Play();
        soundEffect.Play();

        // Spawn and launch the bullet
        Vector3 bulletSpawnPosition = transform.position + transform.forward * 4f + Vector3.up * 3;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, transform.rotation);
        bullet.tag = "Bullet";

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(bulletDirection.normalized * attackForce, ForceMode.Impulse);
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
        enemyHitEvent.Invoke(totalPower);
    }

    private void ChooseWaypoint()
    {
        if (gameEnd || waypoints.Length == 0) return;

        currentWaypointIndex = Random.Range(0, waypoints.Length);
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        agent.isStopped = false;
    }

    private void ResumeAgentMovement()
    {
        if (!agent.isStopped)
            return;

        agent.isStopped = false;
        ChooseWaypoint();
    }
}

