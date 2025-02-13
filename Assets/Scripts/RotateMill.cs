using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMill : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(target.transform.position, Vector3.forward, 300 * Time.deltaTime);
    }
}
