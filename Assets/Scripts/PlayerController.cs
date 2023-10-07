using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    [SerializeField] float camSensitivity = 2f;
    private float xRotation = 0;
    private float yRotation = 0;
    
    //states and inputs 
    private Vector3 horizontal;
    private Vector3 vertical;
    private bool isJumping;
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
        Debug.Log(rb.velocity.magnitude);
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
        Vector3 force = (horizontal + vertical).normalized * (!isGrounded ? 0 : isRunning ? runSpeed : walkSpeed);
        
        //jump if not in the air
        if (isJumping && isGrounded)
        {
            force.y += jumpForce;
            isGrounded = false;
            //change drag when in the air
            rb.drag = airDrag;
        }
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
        isJumping = Input.GetButton("Jump");
    }
    
    private void GetLookInputs()
    {
        xRotation -= Input.GetAxisRaw("Mouse Y") * camSensitivity;
        yRotation += Input.GetAxisRaw("Mouse X") * camSensitivity;
    }

    private void OnCollisionEnter(Collision other)
    {   
        //if player is on the ground, change back drag and allow jump
        if (other.gameObject.CompareTag("Ground"))
        {   
            rb.drag = groundDrag;
            isGrounded = true;
        }
    }
}