using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public static PlayerFinder Instance;
    public Transform CurrentTarget; // Player or Car transform

    void Awake()
    {
        Instance = this;
        Instance.CurrentTarget = GameObject.Find("The Adventurer Blake").transform;

    }

    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }
}
