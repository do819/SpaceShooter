using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveForward : MonoBehaviour
{
    public float speed;
    private Transform target;
    // Use this for initialization
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        try
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch (Exception ex)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        catch (Exception ex)
        {
        }
    }
}
