using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carPrefabs;
    public GameObject carOnePrefab;
    float minSpawnInterval = 10f;
    float maxSpawnInterval = 20f;

    private void Start()
    {
        StartCoroutine(SpawnCarRoutine());
    }

    private void SpawnCar()
    {
        if (carPrefabs.Length == 0) return;

        var randomIndex = Random.Range(0, carPrefabs.Length);
        carOnePrefab = Instantiate(carPrefabs[randomIndex], transform);
        AiCarMovement carScript = carOnePrefab.GetComponent<AiCarMovement>();
        if (carScript != null)
        {
            AssignWaypoints(carScript);
        }
    }

    void AssignWaypoints(AiCarMovement carAI)
    {
        string spawnerName = gameObject.name;
        if (spawnerName.Contains("TopLeft"))
        {
            carAI.SetWayPoints(WayPointManager.Instance.TopLeftWaypoint);
        }
        else if (spawnerName.Contains("TopRight"))
        {
            carAI.SetWayPoints(WayPointManager.Instance.TopRightWaypoint);
        }
        else if (spawnerName.Contains("BotomLeft"))
        {
            carAI.SetWayPoints(WayPointManager.Instance.BottomLeftWayPoints);
        }
    }

    IEnumerator SpawnCarRoutine()
    {
        while (true)
        {
            SpawnCar();
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }
}
