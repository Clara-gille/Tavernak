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
    [SerializeField] private bool inCookingPotRange;

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

       
    }

    void FixedUpdate()
    {
        CookingPotRay();
    }

    void GetActionsInputs()
    {
        isInCookingPot = playerInput.actions["Cooking"].ReadValue<float>() > 0;
    }
    
    private void CookingPotRay()
    {
        //ray from center of the screen
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        //cast the ray
        if (Physics.Raycast(ray, out var hit, cookingPotDistance))
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("CookingPot"))
            {
                inCookingPotRange = true;   
            }

            else {
                inCookingPotRange = false;
            }
        }
        else {
            inCookingPotRange = false;
        }
        
       
    }




}