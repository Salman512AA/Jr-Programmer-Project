using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    [SerializeField] protected Transform[] waypoint;
    [SerializeField] protected int currentWaypointIndex;
   [SerializeField]  protected AudioSource ManScream;
    protected  AudioSource bulletSound;


    public int index;
    protected bool isDead = false;
    // Start is called before the first frame update

    protected virtual void Awake()
    {
        GameObject soundObject = GameObject.Find("BulletHitAi");
        if (soundObject != null)
        {
            ManScream = soundObject.GetComponent<AudioSource>(); 
        }
        GameObject bulletSoundObject = GameObject.Find("BulletFireAi");
        if (bulletSoundObject != null)
        {
            bulletSound = bulletSoundObject.GetComponent<AudioSource>();
        }
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }
    protected virtual void Start()
    {
        if (agent == null)
        {
            Debug.LogError("NavMesh Agent not assigned on " + gameObject.name);
            return; // Exit early if the agent is missing
        }

        // Null check for waypoints and ensure there are at least two
        if (waypoint == null || waypoint.Length < 2)
        {
            Debug.LogError("Waypoints not assigned or insufficient waypoints on " + gameObject.name);
            return;
        }
        agent.SetDestination(waypoint[0].position);
        animator.SetFloat("vertical", 1);
    }
    public void SetManWayPoints(Transform[] RecievedWayPoints)
    {
        this.waypoint = RecievedWayPoints;
        agent.SetDestination(waypoint[0].position);

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        MoveNPC();
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("Tank")|| collision.gameObject.CompareTag("Bullet")) {
            HandleDeath();
            ManScream.Play();

        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandleDeath();
            Destroy(other.gameObject);
            ManScream.enabled = true;
            ManScream.Play();
            isDead = true;
        }
    }
    protected virtual void HandleDeath()
    {
        animator.SetBool("isDead", true);
        StartCoroutine(DeadTime());
    }
    protected virtual void MoveNPC()
    {
        if (agent == null || waypoint == null || waypoint.Length == 0) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentWaypointIndex + 1 >= waypoint.Length - 1)
            {
                Destroy(gameObject);
                return;
            }
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoint.Length;
            agent.SetDestination(waypoint[currentWaypointIndex].position);
        }
    }
    protected virtual IEnumerator DeadTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
