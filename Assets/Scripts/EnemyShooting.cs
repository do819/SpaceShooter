using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
    public GameObject bulletPrefab;
    public float fireDelay = 5f;
    float cooldownTimer = 0;
    float speed = 2f;

   

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime/speed;

        if(cooldownTimer <= 0)
        {
            
            cooldownTimer = fireDelay;
            Vector3 offset = transform.rotation * bulletOffset;

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation);

            bulletGO.layer = gameObject.layer;
             
        }
    }
}
