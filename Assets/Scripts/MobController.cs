using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{   
    [Header("Physical body")]
    [SerializeField] private GameObject body;
    private Rigidbody rb;
    
    [Header("Health")]
    [SerializeField] private float maxHealth = 20f;
    private float currentHealth;
    
    [Header("Movement")]
    [SerializeField] private GameObject [] waypoints;
    [SerializeField] private float waypointRadius = 3f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 2f;
    private int currentWaypoint = 0;
    
    private Ingredient ingredient;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = body.GetComponent<Rigidbody>();
        ingredient = ScriptableObject.CreateInstance<Ingredient>();
        ingredient.name = "Meat";
        // ingredient.img = img;
        // ingredient.stats = stats;
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public Ingredient TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return ingredient;
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
        
        Vector3 directionToWaypoint = waypoints[currentWaypoint].transform.position - body.transform.position;
        Vector3 forward = body.transform.forward;
        
        //angle between the mob's forward and the direction to the waypoint
        float angleToWaypoint = Vector3.SignedAngle(forward, directionToWaypoint, Vector3.up);

        //torque to apply to turn towards the waypoint
        float torqueDivider = 1000; //allows turnSpeed values to be between 1 and 10
        float torque = angleToWaypoint * turnSpeed / torqueDivider;
        rb.AddTorque(Vector3.up * torque, ForceMode.Force);
        
        rb.AddForce(forward * speed, ForceMode.Force);
    }
}
