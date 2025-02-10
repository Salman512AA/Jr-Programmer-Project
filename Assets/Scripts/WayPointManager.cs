using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    public Transform[] TopLeftWaypoint;
    public Transform[] TopRightWaypoint;
    public Transform[] BottomLeftWayPoints;

    //for npc Charachters
    public Transform[] TopLftManWay;
    public Transform[] TopRightManWay;
    public Transform[] BottomLftManWay;

    public static WayPointManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
