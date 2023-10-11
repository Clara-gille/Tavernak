using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;

public class PlayerControllerIndoor : MonoBehaviour
{
    private Rigidbody rb;
    
    [Header("Player Movement")]
    [SerializeField] float walkSpeed = 30f;
    [SerializeField] float runSpeed = 60f;
    [SerializeField] float groundDrag = 5f;
    
    [Header("Camera")]
    public GameObject cam;
    [SerializeField] float camSensitivity = 2f;
    private float xRotation = 0;
    private float yRotation = 0;
    
    
    //states and inputs 
    private Vector3 horizontal;
    private Vector3 vertical;
    private bool isRunning;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = groundDrag;
        
        
        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update()
    {
        //get all inputs
        GetMovementInputs();
        GetLookInputs();
    
    }

    void FixedUpdate()
    {   
        //update player movement and camera
        PlayerMove();
        PlayerLook();
    }
    
    //player body movement and jump
    private void PlayerMove()
    {   
        //force adapt to running or walking and is normalized to avoid diagonal speed boost
        Vector3 force = (horizontal + vertical).normalized * (isRunning ? runSpeed : walkSpeed);
        
        //apply force
        rb.AddForce(force, ForceMode.Impulse);
    }
    
    //player head movement 
    private void PlayerLook()
    {   
        //clamp x rotation to avoid over rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    
    private void GetMovementInputs()
    {
        isRunning = Input.GetButton("Fire3"); //shift 
        horizontal = transform.right *  Input.GetAxis("Horizontal"); 
        vertical   = transform.forward * Input.GetAxis("Vertical");
    }
    
    private void GetLookInputs()
    {
        xRotation -= Input.GetAxisRaw("Mouse Y") * camSensitivity;
        yRotation += Input.GetAxisRaw("Mouse X") * camSensitivity;
    }

}