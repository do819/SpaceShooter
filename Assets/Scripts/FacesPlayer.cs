using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesPlayer : MonoBehaviour
{
    Transform player;

    public float rotSpeed = 90f; 

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            GameObject go = GameObject.Find("Player");

            if(go != null)
            {
                player = go.transform;
            }
        }

        if(player == null)
        {
            return;
        }
        Vector3 dir = player.position - transform.position;

        dir.Normalize();

        float yAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg ;

        Quaternion desiredRot = Quaternion.Euler(0, yAngle, 0 );

        transform.rotation =  Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
    }
}


