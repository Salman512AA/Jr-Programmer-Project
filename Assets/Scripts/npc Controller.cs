using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    [SerializeField] Transform[] waypoint;
    [SerializeField] int currentWaypointIndex; public int index;
    // Start is called before the first frame update

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }
    void Start()
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
    void Update()
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
}
