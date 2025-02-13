using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(buletDestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 70 || transform.position.x < -70 || transform.position.z > 70 || transform.position.z < -70)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            
            Destroy(gameObject);
            Debug.Log("Bullet hit");
        }
    }
    
    IEnumerator buletDestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
