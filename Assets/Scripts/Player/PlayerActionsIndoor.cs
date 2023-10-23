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
    [SerializeField] public GameObject CookingCanvas;
    [SerializeField] private bool inventoryOpened = false;


    [Header("Camera")]
    public GameObject cam;

   
    [Header("Cooking pot")] 
    [SerializeField] private GameObject cookingPot;
    [SerializeField] private float cookingPotDistance = 3f;
    private bool isInCookingPotRange = false;


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
        //cooking need to be in the range of the cooking pot
        isInCookingPotRange = Vector3.Distance(transform.position, cookingPot.transform.position) < cookingPotDistance;

       
    }
   
    private void Cooking()
    {
        if (isInCookingPotRange)
        {
            isInCookingPot = true;
            CookingCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            playerInput.SwitchCurrentActionMap("Cooking");
        }
        
        
    }

    private void StopCooking()
    {

        isInCookingPot = false;
        CookingCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput.SwitchCurrentActionMap("Indoor");
    }



}