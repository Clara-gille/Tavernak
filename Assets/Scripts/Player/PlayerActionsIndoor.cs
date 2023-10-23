using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerControllerIndoor : MonoBehaviour
{

    [SerializeField] public CanvasGroup InventoryCanvas;
    [SerializeField] private bool inventoryOpened = false;


    [Header("Camera")]
    public GameObject cam;
   
    [Header("In cooking pot")] 
    [SerializeField] private float cookingPotDistance = 3f;

    [Header ("Inputs")]
    [SerializeField] PlayerInput playerInput;
    private bool isInCookingPot = false;

    
    
    [SerializeField] private GameObject inventoryManagerObj;
    private InventoryManager inventoryManager;

    

    void Start()
    {
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
    }

   void Update()
    {
        GetActionsInputs();


        if (isInCookingPot && CookingPotRay)
        { // Opening the inventory thank to the key in inputmanager
            inventoryOpened = !inventoryOpened;
        }

        if (!inventoryOpened)
        {
            InventoryCanvas.alpha = 0.0f; //hide the inventory
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            InventoryCanvas.alpha = 1f;         //show the inventory
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        CookingPotRay();
    }

    void GetActionsInputs()
    {
        isInCookingPot = playerInput.actions["Cooking"].ReadValue<float>() > 0;
    }
    
    private bool CookingPotRay()
    {
        //ray from center of the screen
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        //cast the ray
        if (Physics.Raycast(ray, out var hit, cookingPotDistance))
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("CookingPot"))
            {
                return true;   
            }

            else return false;
        }
        else return false;
        
       
    }




}