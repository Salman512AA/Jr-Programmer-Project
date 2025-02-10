using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class CarTrigger : MonoBehaviour
{
    public GameObject player;        // Reference to the player
    private CarController carController; // Car controller script attached to the car
    private AiCarMovement aiController;   // AI car movement script attached to the car
    private bool isPlayerInCar = false;   // Flag to track if the player is in the car
    private bool isPlayerNearCar = false; // Flag to check if the player is near the car
    private NavMeshAgent navMeshAgent;  // NavMeshAgent attached to the car (AI car movement)
    private CinemachineVirtualCamera carVCam; // Car's Cinemachine camera
    private CinemachineVirtualCamera playerVCam; // Player's default camera
    public Transform mainCamera; // Assign the Main Camera

    // Start is called before the first frame update
    private void Start()
    {
        // Initially find the car's camera
        carVCam = GetComponentInChildren<CinemachineVirtualCamera>();

        // Get references to car components (CarController, AiCarMovement, NavMeshAgent)
        carController = GetComponent<CarController>();
        aiController = GetComponent<AiCarMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        mainCamera= player.transform.Find("Main Camera");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player is near the car
        if (other.CompareTag("Player"))
        {
            isPlayerNearCar = true; // Player is near the car

            // Now fetch the references for player and car only when the player enters the trigger zone
            player = GameObject.Find("The Adventurer Blake");

            if (player != null)
            {
                // Reference to player camera (PlayerVCam) only when player enters
                playerVCam = GameObject.Find("PlayerVCam")?.GetComponent<CinemachineVirtualCamera>();

                if (playerVCam == null)
                {
                    Debug.LogError("PlayerVCam not found as child of player!");
                }
                else
                {
                    Debug.Log("PlayerVCam found!");
                }
            }
            else
            {
                Debug.LogError("Player GameObject not found. Make sure the name is correct and the player is in the scene.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player leaves the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearCar = false; // Player is no longer near the car
            Debug.Log("Player exited trigger zone");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Only allow interaction when the player is near the car
        if (isPlayerNearCar && Input.GetKeyDown(KeyCode.E))
        {
            if (isPlayerInCar)
            {
                ExitCar();
            }
            else
            {
                EnterCar();
            }
        }
    }

    private void EnterCar()
    {
        // Disable AI movement if necessary
        if (navMeshAgent != null) navMeshAgent.enabled = false;

        // Deactivate the player and activate the car
        carController.enabled = true;
        aiController.enabled = false;

        // Ensure car camera is active
        carVCam.gameObject.SetActive(true);  // Activate the car's camera
        carVCam.Priority = 20;

        // Set the player camera's priority lower, making sure it's inactive
        if (playerVCam != null)
        {
            playerVCam.Priority = 5; // Lower priority
            playerVCam.gameObject.SetActive(false); // Deactivate the player's camera
        }

        isPlayerInCar = true;
        Debug.Log("Player entered the car");
        DisablePlayer();
    }

    private void ExitCar()
    {
        // Reactivate AI movement
        if (navMeshAgent != null) navMeshAgent.enabled = true;

        // Re-enable the player and position them near the car
        EnablePlayer();
        player.transform.position = transform.position + new Vector3(2, 0, 0); // Adjust position

        // Ensure player camera is active when exiting
        if (playerVCam != null)
        {
            playerVCam.gameObject.SetActive(true); // Activate player camera again
            playerVCam.Priority = 20; // Set priority higher
        }

        // Switch cameras: Lower the car's priority and deactivate its camera
        carVCam.Priority = 5;
        carVCam.gameObject.SetActive(false); // Deactivate car camera

        // Disable car controls
        carController.enabled = false;
        aiController.enabled = true;

        isPlayerInCar = false;

        Debug.Log("Player exited the car");
    }
    public void DisablePlayer()
    {
        // Unparent the camera before disabling the player
      //  mainCamera.transform.parent = null;
        player.SetActive(false);
    }

    public void EnablePlayer()
    {
        player.SetActive(true); // Reactivate the player

        // Reparent the camera back to the player
       // mainCamera.transform.parent = player.transform;
    }
}



