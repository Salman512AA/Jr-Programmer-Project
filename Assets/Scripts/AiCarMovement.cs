using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCarMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField]  Transform[] waypoint;
    [SerializeField] int currentWaypointIndex;
    [SerializeField] float stopTimeThreshold = 5f; // Time before destroying the car
    private float stopTimer = 1f; // Timer to track how long the car has been stopped

    // Start is called before the first frame update
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
        agent.speed = Random.Range(4f, 7f);
    }
    public void SetWayPoints(Transform[] RecievedWayPoints)
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
            if (currentWaypointIndex + 1 >= waypoint.Length-1){
                Destroy(gameObject);
                return;
            }
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoint.Length;
            agent.SetDestination(waypoint[currentWaypointIndex].position);
        }
        Vector3 rayOrigin = transform.position + Vector3.up;
        Ray ray = new Ray(rayOrigin, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 7f, Color.blue); // Always draw blue ray

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 7))
        {
            Debug.DrawRay(ray.origin, ray.direction * 7, Color.red); // If hit, make it red
            if (raycastHit.collider.gameObject.CompareTag("Car"))
            {
                float distanceToCar = Vector3.Distance(transform.position, raycastHit.collider.transform.position);

                if (distanceToCar < 7f) // Stop if the car is too close (adjust the threshold as needed)
                {
                    agent.isStopped = true;
                    stopTimer += Time.deltaTime;

                    // If the car has been stopped for longer than the threshold, destroy it
                    if (stopTimer >= stopTimeThreshold)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.green); // If no hit, make it green
            if (agent.isStopped)
            {
                agent.isStopped = false;
                stopTimer = 0f;

            }
        }


    }
}
