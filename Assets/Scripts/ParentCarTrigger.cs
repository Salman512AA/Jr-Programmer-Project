using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ParentCarTrigger : MonoBehaviour
{
    public GameObject player;        // Reference to the player
    protected bool isPlayerInCar = false;   // Flag to track if the player is in the car
    protected bool isPlayerNearCar = false; // Flag to check if the player is near the car
    protected CinemachineVirtualCamera carVCam; // Car's Cinemachine camera
    protected CinemachineVirtualCamera playerVCam; // Player's default camera

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Initially find the car's camera
        carVCam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearCar = true;
            player = GameObject.Find("The Adventurer Blake");

            if (player != null)
            {
                playerVCam = GameObject.Find("PlayerVCam")?.GetComponent<CinemachineVirtualCamera>();

                if (playerVCam == null)
                {
                    Debug.LogError("PlayerVCam not found!");
                }
            }
            else
            {
                Debug.LogError("Player GameObject not found.");
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearCar = false;
            Debug.Log("Player exited trigger zone");
        }
    }

    protected virtual void Update()
    {
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

    protected virtual void EnterCar()
    {
        carVCam.Priority = 20;

        if (playerVCam != null)
        {
            playerVCam.Priority = 5;
            playerVCam.gameObject.SetActive(false);
        }

        isPlayerInCar = true;
        player.SetActive(false);
        //PlayerFinder.Instance.SetTarget(this.transform); // Set to car
        gameObject.AddComponent<AudioListener>();
        AudioListener playerAudioListener = player.GetComponent<AudioListener>();
        if (playerAudioListener != null)
        {
            Destroy(playerAudioListener); // Remove player's AudioListener
        }

        Debug.Log("Player entered the car");
    }

    protected virtual void ExitCar()
    {
        player.SetActive(true);
        player.transform.position = transform.position + new Vector3(2, 0, 0);

        if (playerVCam != null)
        {
            playerVCam.gameObject.SetActive(true);
            playerVCam.Priority = 20;
        }
        player.AddComponent<AudioListener>();
        AudioListener carAudioListener = GetComponent<AudioListener>();
        if (carAudioListener != null)
        {
            Destroy(carAudioListener); // Remove car's AudioListener
        }
        carVCam.Priority = 5;
        isPlayerInCar = false;
       // PlayerFinder.Instance.SetTarget(player.transform); // Set back to player

        Debug.Log("Player exited the car");
    }
}
