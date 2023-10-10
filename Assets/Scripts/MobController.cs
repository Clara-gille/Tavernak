using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{   
    [Header("Physical body")]
    [SerializeField] private GameObject body;
    private Rigidbody rb;
    
    [Header("Health and drops")]
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private string drop = "meat";
    private float currentHealth;
    
    [Header("Movement")]
    [SerializeField] private GameObject [] waypoints;
    [SerializeField] private float waypointRadius = 3f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 2f;
    private int currentWaypoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = body.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public string TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return drop;
        }
        return null;
    }

    private void Move()
    {
        if(Vector3.Distance(body.transform.position, waypoints[currentWaypoint].transform.position) < waypointRadius)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            
            
        }
        
        Quaternion lookAtWp = Quaternion.LookRotation(waypoints[currentWaypoint].transform.position - body.transform.position);
        float turnSpeedDivider = 200; //allows turnSpeed values to be between 1 and 10
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, lookAtWp, turnSpeed / turnSpeedDivider);
        
        
        rb.AddForce(body.transform.forward * speed, ForceMode.Impulse);
    }
}
