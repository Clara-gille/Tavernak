using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerIndoor : MonoBehaviour
{

    [Header("Camera")]
    public GameObject cam;
   
    [Header("In cooking pot")] 
    [SerializeField] private float cookingPotDistance = 3f;
    
    [Header ("Inputs")]
    [SerializeField] PlayerInput playerInput;
    private bool isInCookingPot;
    
    
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
           //open the inventory

        }

    }


}