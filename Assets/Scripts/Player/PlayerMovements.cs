using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerMovements : MonoBehaviour
{   
    private Rigidbody rb;
    
    [Header("Player Movement")]
    [SerializeField] float walkSpeed = 30f;
    [SerializeField] float runSpeed = 60f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] float groundDrag = 5f;
    [SerializeField] float airDrag = 0.5f;
    
    [Header("Camera")]
    public GameObject cam;
    [SerializeField] float camSensitivity = 0.1f;
    private float xRotation = 0;
    private float yRotation = 0;
    
    //states and inputs 
    [Header ("Inputs")]
    [SerializeField] PlayerInput playerInput;
    private Vector2 movementInput;
    private bool isRunning; 
    private bool isGrounded = false;
    
    

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

        
        //change drag when in the air
        rb.drag = isGrounded ? groundDrag : airDrag;
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
        Vector3 horizontal = transform.right * movementInput.x;
        Vector3 vertical = transform.forward * movementInput.y;
        Vector3 force = (horizontal + vertical).normalized * (!isGrounded ? 0 : isRunning ? runSpeed : walkSpeed);
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
        isRunning = playerInput.actions["Run"].ReadValue<float>() > 0;
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }
    
    private void GetLookInputs()
    {
        Vector2 lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        xRotation -= lookInput.y * camSensitivity;
        yRotation += lookInput.x * camSensitivity;
    }

    private void OnCollisionEnter(Collision other)
    {   
        //if player is on the ground, change back drag and allow jump
        if (other.gameObject.CompareTag("Ground"))
        {   
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {   
        //if player is not on the ground, change drag and disable jump
        if (other.gameObject.CompareTag("Ground"))
        {   
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }
}