using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] manPrefabs;
    public GameObject manOnePrefab;
    float minManSpawnTime = 15f;
    float maxManSpawnTime = 30f;

    private void Start()
    {
        StartCoroutine(SpawnManRoutine());
    }

    private void SpawnMan()
    {
        if (manPrefabs.Length == 0) return;

        var randomIndex = Random.Range(0, manPrefabs.Length);
        manOnePrefab = Instantiate(manPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
        npcController manScript = manOnePrefab.GetComponent<npcController>();
        if (manScript != null)
        {
            AssignManWaypoints(manScript);
        }
    }

    void AssignManWaypoints(npcController manScript)
    {
        string spawnerName = gameObject.name;
        if (spawnerName.Contains("TopLeft"))
        {
            manScript.SetManWayPoints(WayPointManager.Instance.TopLftManWay);
        }
        else if (spawnerName.Contains("TopRight"))
        {
            manScript.SetManWayPoints(WayPointManager.Instance.TopRightManWay);
        }
        else if (spawnerName.Contains("BotomLeft"))
        {
            manScript.SetManWayPoints(WayPointManager.Instance.BottomLftManWay);
        }
    }

    IEnumerator SpawnManRoutine()
    {
        while (true)
        {
            SpawnMan();
            yield return new WaitForSeconds(Random.Range(minManSpawnTime, maxManSpawnTime));
        }
    }
}

