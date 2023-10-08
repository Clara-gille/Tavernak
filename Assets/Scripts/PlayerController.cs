using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    [Header("Sword")]
    [SerializeField] private GameObject sword;
    private Animator swordAnimator;
    [SerializeField] private float swordRange = 3f;
    [SerializeField] private float swordDamage = 10f;
    [SerializeField] private float swordInterval = 0.667f; //length of swing animation
    [SerializeField] private float damageDelay = 0.2f; //delay before animation reaches peak and damage is dealt
    private bool canSwing = true;
    
    [Header("Pick Up")] 
    [SerializeField] private float pickUpDistance = 3f;
    [SerializeField] private TextMeshProUGUI pickUpText;
    
    //states and inputs 
    private Vector3 horizontal;
    private Vector3 vertical;
    private bool isJumping;
    private bool isRunning;
    private bool isPickingUp;
    private bool isAttacking;
    
    private bool isGrounded = false;
    private static readonly int Swing = Animator.StringToHash("Swing");
    private static readonly int IsSwinging = Animator.StringToHash("isSwinging");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = groundDrag;
        
        swordAnimator = sword.GetComponent<Animator>();
        
        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    private void Update()
    {
        //get all inputs
        GetMovementInputs();
        GetLookInputs();
        
        PickUpRay();
        SwordHandler();
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
        isPickingUp = Input.GetButton("Fire2"); //right click
        isAttacking = Input.GetButton("Fire1"); //left click
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

    private void PickUpRay()
    {
        //ray from center of the screen
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        //cast the ray
        if (Physics.Raycast(ray, out var hit, pickUpDistance))
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Collectible"))
            {
                pickUpText.gameObject.SetActive(true);
                if (isPickingUp)
                {
                    CollectibleController collectible = other.GetComponent<CollectibleController>();
                    collectible.PickUp();
                }
                
            }
        }
        else
        {
            pickUpText.gameObject.SetActive(false);
        }
    }

    private void SwordHandler()
    {
        //sword attack animation
        swordAnimator.SetBool(IsSwinging, isAttacking);
        if (isAttacking && canSwing)
        {
            StartCoroutine(SwordAttack());
        }
    }

    IEnumerator SwordAttack()
    {   
        canSwing = false;
        yield return new WaitForSeconds(damageDelay); //wait for animation to reach peak
        //ray from center of the screen
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        //cast the ray
        if (Physics.Raycast(ray, out var hit, swordRange))
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Mob"))
            {
                MobController mob = other.GetComponent<MobController>();
                String drop = mob.TakeDamage(swordDamage);
                if (drop != null)
                {
                    //drop item
                    Debug.Log("acquired " + drop);
                }
            }
        }
        yield return new WaitForSeconds(swordInterval - damageDelay); //wait for animation to finish
        canSwing = true;
    }

}