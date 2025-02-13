using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestriyed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(buletDestroyTimer());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator buletDestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
